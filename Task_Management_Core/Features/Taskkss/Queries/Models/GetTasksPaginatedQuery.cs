using MediatR;
using Task_Management_Core.Features.Taskkss.Queries.Results;
using Task_Management_Core.Wrapper;

namespace Task_Management_Core.Features.Taskkss.Queries.Models
{
    public class GetTasksPaginatedQuery : IRequest<PaginatedResult<GetTaskResult>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
