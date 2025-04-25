using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Taskkss.Queries.Results;

namespace Task_Management_Core.Features.Taskkss.Queries.Models
{
    public class GetUserAllTasksQuery : IRequest<Response<List<GetAllTasksResult>>>
    {
        /*public string UserId { get; set; }

        public GetUserAllTasksQuery(string userId)
        {
            UserId = userId;
        }*/
    }
}
