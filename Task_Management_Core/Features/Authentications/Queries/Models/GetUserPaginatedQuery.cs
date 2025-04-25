using MediatR;
using Task_Management_Core.Features.Authentications.Queries.Results;
using Task_Management_Core.Wrapper;

namespace Task_Management_Core.Features.Authentications.Queries.Models
{
    public class GetUserPaginatedQuery : IRequest<PaginatedResult<GetUserPaginatedList>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
