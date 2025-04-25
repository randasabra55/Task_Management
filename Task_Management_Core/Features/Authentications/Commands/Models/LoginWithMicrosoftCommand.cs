using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Data.Results;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class LoginWithMicrosoftCommand : IRequest<Response<JwtAuthResult>>
    {
        public string Token { get; set; }
    }
}
