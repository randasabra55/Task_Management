using MediatR;
using Task_Management_Core.Features.Reviewss.Queries.Results;
using Task_Management_Core.Wrapper;

namespace Task_Management_Core.Features.Reviewss.Queries.Models
{
    public class GetReviewsPaginatedAboutTaskQuery : IRequest<PaginatedResult<GetReviewsPaginatedResult>>
    {
        public int TaskId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetReviewsPaginatedAboutTaskQuery(int id)
        {
            TaskId = id;
        }
    }
}
