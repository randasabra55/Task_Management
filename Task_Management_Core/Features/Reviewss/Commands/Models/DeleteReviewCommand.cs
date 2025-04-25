
using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Reviewss.Commands.Models
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteReviewCommand(int id)
        {
            Id = id;
        }
    }
}
