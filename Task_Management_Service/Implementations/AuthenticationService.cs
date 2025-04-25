using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Task_Management_Data.Entities.Identity;
using Task_Management_Data.Helper;
using Task_Management_Data.Results;
using Task_Management_Infrastructure.Data;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        UserManager<User> userManager;
        HttpClient httpClient;
        JwtSettings jwtSettings;
        /*IOTPRepository otpRepository;*/
        Context context;
        public AuthenticationService(UserManager<User> userManager, JwtSettings jwtSettings, Context context, HttpClient httpClient)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
            //this.otpRepository = otpRepository;
            this.context = context;
            this.httpClient = httpClient;
        }

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            //design token
            var accessToken = new JwtSecurityToken(
                issuer: jwtSettings.issuer,
                audience: jwtSettings.audience,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.secret)), SecurityAlgorithms.HmacSha256Signature),
                claims: await GetClaims(user)
                );
            RefreshToken refreshToken = new RefreshToken()
            {
                UserName = user.UserName,
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                Token = GenerateRefreshToken(),
            };
            JwtAuthResult jwtAuthResult = new JwtAuthResult()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                refreshToken = refreshToken
            };
            return jwtAuthResult;

        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var Rules = await userManager.GetRolesAsync(user);
            foreach (var rule in Rules)
            {
                claims.Add(new Claim(ClaimTypes.Role, rule));
            }

            return claims;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var genrator = RandomNumberGenerator.Create();
            genrator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> Register(User user, string password)
        {

            //chek email exist or not
            var existEmail = await userManager.FindByEmailAsync(user.Email);
            if (existEmail != null)
            {
                return "EmailIsExist";
            }
            //check user name
            var existUserName = await userManager.FindByNameAsync(user.UserName);
            if (existUserName != null)
            {
                return "UserNameIsExist";
            }
            try
            {
                user.Role = "Employee";
                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    return string.Join(",", result.Errors.Select(x => x.Description).ToList());
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.InnerException?.Message ?? ex.Message}";

            }


            //add him to role
            await userManager.AddToRoleAsync(user, "Employee");
            return "Created";

        }

        public async Task<string> LoginWithGoogleAsync(string tokenId, string googleAccessToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId);
            if (payload == null)
                return "Invalid Google Token";
            var user = userManager.Users.FirstOrDefault(u => u.Email == payload.Email);
            if (user == null)
            {
                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    FullName = payload.Email,
                    EmailConfirmed = true,
                    GoogleAccessToken = googleAccessToken
                };
                user.Role = "Employee";
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return string.Join(",", result.Errors.Select(x => x.Description).ToList());
                }
            }
            else
            {
                user.GoogleAccessToken = googleAccessToken;
                await userManager.UpdateAsync(user);
            }
            var accessToken = new JwtSecurityToken(
                issuer: jwtSettings.issuer,
                audience: jwtSettings.audience,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.secret)), SecurityAlgorithms.HmacSha256Signature),
                claims: await GetClaims(user)
                );
            /*user.GoogleAccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
            await userManager.UpdateAsync(user);*/
            return new JwtSecurityTokenHandler().WriteToken(accessToken);

        }

        public async Task<JwtAuthResult> LoginWithMicrosoftAsync(string microsoftAccessToken)
        {
            httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", microsoftAccessToken);

            var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/me");

            if (!response.IsSuccessStatusCode)
            {
                return new JwtAuthResult
                {
                    AccessToken = $"Unable to retrieve Microsoft user info "

                };
            }


            var content = await response.Content.ReadAsStringAsync();
            var microsoftUser = JsonSerializer.Deserialize<MicrosoftUser>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (microsoftUser == null || string.IsNullOrEmpty(microsoftUser.Mail))
            {
                return new JwtAuthResult
                {
                    AccessToken = $"Unable to retrieve Microsoft user info +{string.Empty}"

                };
            }

            var user = await userManager.FindByEmailAsync(microsoftUser.Mail);

            if (user == null)
            {
                user = new User
                {
                    Email = microsoftUser.Mail,
                    FullName = microsoftUser.DisplayName,
                    UserName = microsoftUser.Mail

                };
                user.Role = "Employee";

                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create user: {errors}");
                }

            }

            var jwt = await GetJWTToken(user);

            return jwt;
        }
    }



    /* public async Task<string> GenerateOTPCode(string Email)
     {
         var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == Email);
         if (user == null)
         {
             return "User not found";
         }
         var code = new Random().Next(100000, 999999).ToString();
         //otpRepository.FindOTPinDatabase(user);
         var otpCode = new OTP()
         {
             UserId = user.Id,
             Code = code,
             ExpireAt = DateTime.Now.AddMinutes(5),
             // IsUsed = false
         };
         await otpRepository.AddAsync(otpCode);
         await SendOtpByEmail(Email, code);
         return code;
         //throw new NotImplementedException();
     }*/

    /*private async Task SendOtpByEmail(string email, string otp)
    {
        var fromAddress = new MailAddress("randasabra27@gmail.com");
        var toAddress = new MailAddress(email);
        const string fromPassword = "gzqz fpnp oixd ljdo";
        const string subject = "Password Reset OTP";
        string body = $"Your OTP Code is: {otp}. It will expire in 5 minutes.";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            await smtp.SendMailAsync(message);
        }
    }*/

    /*private async Task SendOtpByEmail(string email, string otp)
    {
        try
        {
            var fromAddress = new MailAddress("randasabra27@gmail.com", "Your App Name");
            var toAddress = new MailAddress(email);
            const string fromPassword = "gzqz fpnp oixd ljdo";
            const string subject = "Password Reset OTP";
            string body = $"Your OTP Code is: {otp}. It will expire in 5 minutes.";

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);

                using (var message = new MailMessage())
                {
                    message.From = fromAddress;
                    message.To.Add(toAddress);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = false;

                    await smtp.SendMailAsync(message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }*/

    /*public async Task<string> VerifyOTPCode(string email, string code)
    {
        var user = await userManager.FindByEmailAsync(email);
        var codeFromDatabase = context.OTPs.FirstOrDefault(o => o.UserId == user.Id && !o.IsUsed && o.Code == code);
        if (codeFromDatabase == null)
            return "Invalid OTP or expired";
        else if (codeFromDatabase.ExpireAt < DateTime.UtcNow)
            return "OTP has expired";
        codeFromDatabase.IsUsed = true;
        await context.SaveChangesAsync();

        return "Success";
    }

    public async Task<string> ResetPassword(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return "UserNotFound";
        await userManager.RemovePasswordAsync(user);
        if (!await userManager.HasPasswordAsync(user))
        {
            await userManager.AddPasswordAsync(user, password);
        }

        return "Success";
    }*/


}

