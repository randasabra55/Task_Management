using MediatR;
using Task_Management_Core.Features.Projectss.Queries.Results;
using Task_Management_Core.Wrapper;

namespace Task_Management_Core.Features.Projectss.Queries.Models
{
    public class GetProjectsPaginatedQuery : IRequest<PaginatedResult<GetProjectResult>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
