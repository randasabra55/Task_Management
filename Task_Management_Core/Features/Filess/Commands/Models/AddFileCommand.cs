using MediatR;
using Microsoft.AspNetCore.Http;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Filess.Commands.Models
{
    public class AddFileCommand : IRequest<Response<string>>
    {
        public string FileName { get; set; }
        public IFormFile FileURL { get; set; }
        public int TaskId { get; set; }
    }
}
