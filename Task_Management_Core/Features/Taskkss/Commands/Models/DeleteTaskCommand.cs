using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Taskkss.Commands.Models
{
    public class DeleteTaskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteTaskCommand(int id)
        {
            Id = id;
        }
    }
}
