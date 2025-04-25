
using Task_Management_Data.Entities.Identity;
using Task_Management_Data.Results;

namespace Task_Management_Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> Register(User user, string password);
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<string> LoginWithGoogleAsync(string tokenId, string googleAccessToken);
        public Task<JwtAuthResult> LoginWithMicrosoftAsync(string microsoftAccessToken);


        /*public Task<string> GenerateOTPCode(string Email);
        public Task<string> VerifyOTPCode(string email, string code);
        public Task<string> ResetPassword(string email, string password);*/
    }
}
