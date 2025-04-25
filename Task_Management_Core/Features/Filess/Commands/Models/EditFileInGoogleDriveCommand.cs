using MediatR;
using Microsoft.AspNetCore.Http;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Filess.Commands.Models
{
    public class EditFileInGoogleDriveCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public IFormFile FileURL { get; set; }
    }

}
