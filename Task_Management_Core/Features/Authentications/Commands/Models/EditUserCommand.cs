using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class EditUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
