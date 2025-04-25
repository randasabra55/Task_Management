using MediatR;
using Task_Management_Core.Bases;


namespace Task_Management_Core.Features.Reviewss.Commands.Models
{
    public class AddReviewCommand : IRequest<Response<string>>
    {
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
    }
}
