using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Data.Results;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class LoginCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
