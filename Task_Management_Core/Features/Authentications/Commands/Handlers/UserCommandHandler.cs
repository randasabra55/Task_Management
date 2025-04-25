using AutoMapper;

//using E_Learning_Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Authentications.Commands.Models;
using Task_Management_Data.Entities.Identity;
using Task_Management_Data.Results;
using Task_Management_Service.Abstracts;

namespace Task_Management_Core.Features.Authentications.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
                                    IRequestHandler<AddUserCommand, Response<string>>,
                                    IRequestHandler<LoginCommand, Response<JwtAuthResult>>,
                                    IRequestHandler<ChangePasswordCommand, Response<string>>,
                                    IRequestHandler<EditUserCommand, Response<string>>,
                                    IRequestHandler<DeleteUserCommand, Response<string>>,
                                    IRequestHandler<LoginWithGoogleCommand, Response<string>>,
                                    IRequestHandler<LoginWithMicrosoftCommand, Response<JwtAuthResult>>,
                                    IRequestHandler<DeleteUserByTokenCommand, Response<string>>


    {
        IAuthenticationService authenticationService;
        IMapper mapper;
        UserManager<User> userManager;
        IHttpContextAccessor httpContextAccessor;
        public UserCommandHandler(IAuthenticationService authenticationService, IMapper mapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.authenticationService = authenticationService;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);

            var result = await authenticationService.Register(user, request.Password);


            switch (result)
            {
                case "EmailIsExist": return BadRequest<string>("Email is Exist");
                case "UserNameIsExist": return BadRequest<string>("UserName is Exist");
                case "Created": return Success<string>("Registed succefully");
                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<JwtAuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var userPassword = await userManager.CheckPasswordAsync(user, request.Password);
                if (userPassword)
                {
                    //generate token
                    var result = await authenticationService.GetJWTToken(user);
                    //return Token 
                    return Success(result);

                }
            }
            return BadRequest<JwtAuthResult>("UserName or password wrong");

        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return BadRequest<string>("user not found");
            IdentityResult result = await userManager.ChangePasswordAsync(user, request.OldPass, request.NewPass);
            if (result.Succeeded)
                return Success<string>("Password changed succefully");
            return BadRequest<string>("can not change your password, please try again");
            //throw new NotImplementedException();
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>("user not found");

            var newUser = mapper.Map(request, oldUser);

            var userByUserName = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            if (userByUserName != null) return BadRequest<string>("UserName is exist");
            var userByFullName = await userManager.Users.FirstOrDefaultAsync(x => x.FullName == newUser.FullName && x.Id != newUser.Id);
            if (userByFullName != null) return BadRequest<string>("FullName is exist before");


            var result = await userManager.UpdateAsync(newUser);
            if (!result.Succeeded) return BadRequest<string>("Failed to edit user");

            return Success("User edited successfully");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                /*await userManager.VerifyUserTokenAsync(request.Id.ToString());*/
                var user = await userManager.FindByIdAsync(request.Id.ToString());
                if (user == null) return NotFound<string>("User not found");
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Any())
                {
                    var roleRemovalResult = await userManager.RemoveFromRolesAsync(user, roles);
                    if (!roleRemovalResult.Succeeded)
                    {
                        var errors = string.Join(", ", roleRemovalResult.Errors.Select(e => e.Description));
                        return BadRequest<string>($"Error removing roles: {errors}");
                    }
                }

                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest<string>($"Unable to delete user: {errors}");
                }

                return Success("User deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<Response<string>> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.LoginWithGoogleAsync(request.tokenId, request.GoogleAccessToken);
            return Success(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(LoginWithMicrosoftCommand request, CancellationToken cancellationToken)
        {
            var res = await authenticationService.LoginWithMicrosoftAsync(request.Token);
            return Success(res);
        }

        public async Task<Response<string>> Handle(DeleteUserByTokenCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized<string>("User is not authenticated. Please login and try again.");

                return BadRequest<string>("Invalid Token or User not authenticated.");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return NotFound<string>("User not found");
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var roleRemovalResult = await userManager.RemoveFromRolesAsync(user, roles);
                if (!roleRemovalResult.Succeeded)
                {
                    var errors = string.Join(", ", roleRemovalResult.Errors.Select(e => e.Description));
                    return BadRequest<string>($"Error removing roles: {errors}");
                }
            }
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest<string>($"Unable to delete user: {errors}");
            }
            return Success("User deleted successfully");
        }
    }
}
