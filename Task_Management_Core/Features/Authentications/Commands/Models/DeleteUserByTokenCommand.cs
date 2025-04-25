using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class DeleteUserByTokenCommand : IRequest<Response<string>>
    {
        public string token { get; set; }
    }
}
