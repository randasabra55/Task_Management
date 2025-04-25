using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Reviewss.Queries.Results;

namespace Task_Management_Core.Features.Reviewss.Queries.Models
{
    public class GetReviewByIdQuery : IRequest<Response<GetReviewsPaginatedResult>>
    {
        public int Id { get; set; }
        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }
    }
}
