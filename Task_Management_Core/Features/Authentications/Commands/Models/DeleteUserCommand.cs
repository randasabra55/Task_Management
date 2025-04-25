using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }
}
