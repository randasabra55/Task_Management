using MediatR;
using Task_Management_Core.Bases;

namespace Task_Management_Core.Features.Reviewss.Commands.Models
{
    public class EditReviewCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
