using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
