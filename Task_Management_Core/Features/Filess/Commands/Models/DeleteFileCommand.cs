using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Filess.Commands.Models
{
    public class DeleteFileCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteFileCommand(int id)
        {
            Id = id;
        }
    }
}
