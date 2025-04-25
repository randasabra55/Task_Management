using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Taskkss.Queries.Results;

namespace Task_Management_Core.Features.Taskkss.Queries.Models
{
    public class GetTasksForSpecificProjectQuery : IRequest<Response<List<GetTasksForSpecificProjectResult>>>
    {
        public int projectId { get; set; }
        public GetTasksForSpecificProjectQuery(int projectId)
        {
            this.projectId = projectId;
        }
    }
}
