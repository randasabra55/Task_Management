using MediatR;
using Task_Management_Core.Features.Filess.Queries.Results;
using Task_Management_Core.Wrapper;

namespace Task_Management_Core.Features.Filess.Queries.Models
{
    public class GetAllFilesForTaskQuery : IRequest<PaginatedResult<GetFileResult>>
    {
        public int TaskId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllFilesForTaskQuery(int taskId)
        {
            this.TaskId = taskId;
        }
    }
}
