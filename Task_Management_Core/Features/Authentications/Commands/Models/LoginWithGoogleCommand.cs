using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Authentications.Commands.Models
{
    public class LoginWithGoogleCommand : IRequest<Response<string>>
    {
        public string tokenId { get; set; }
        public string GoogleAccessToken { get; set; }

    }
}
