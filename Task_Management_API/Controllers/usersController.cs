using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Authentications.Commands.Models;
using Task_Management_Core.Features.Authentications.Queries.Models;

namespace E_Learning_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : AppControllerBase
    {

        //[HttpPost("Register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AddUserCommand addUser)
        {

            var result = await Mediator.Send(addUser);
            return NewResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> RegisterWithGoogleAsync(LoginWithGoogleCommand addUser)
        {

            var result = await Mediator.Send(addUser);
            return NewResult(result);
        }

        [HttpPost("login/microsoft")]
        public async Task<IActionResult> RegisterWithMicrosoftAsync([FromBody] LoginWithMicrosoftCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }



        [HttpPut("me/password")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }



        //  [Authorize]
        //[HttpPut("EditProfile")]
        [HttpPut("me")]
        public async Task<IActionResult> EditUserAsync(EditUserCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //  [Authorize(Roles = "Admin")]
        /* [HttpDelete("DeleteUser/{id}")]
         public async Task<IActionResult> DeleteUserAsync([FromRoute] string id)
         {
             var result = await Mediator.Send(new DeleteUserCommand(id));
             return NewResult(result);
         }*/


        /*[HttpDelete("DeleteUser")]*/
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteUserByTokenAsync()
        {
            var result = await Mediator.Send(new DeleteUserByTokenCommand());
            return NewResult(result);
        }

        //   [Authorize(Roles = "Admin")]
        //[HttpGet("GetUserByToken")]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserByIdAsync()
        {
            var result = await Mediator.Send(new GetUserByIdQuery());
            return NewResult(result);
        }

        //    [Authorize(Roles = "Admin")]
        //[HttpGet("Paginated")]
        [HttpGet]
        public async Task<IActionResult> GetUserPaginatedListAsync([FromQuery] GetUserPaginatedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
