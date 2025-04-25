using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Projectss.Commands.Models
{
    public class DeleteProjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }
    }
}
