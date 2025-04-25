using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
    }
}
