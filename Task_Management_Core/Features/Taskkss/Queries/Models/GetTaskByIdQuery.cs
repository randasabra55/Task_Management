using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Taskkss.Queries.Results;

namespace Task_Management_Core.Features.Taskkss.Queries.Models
{
    public class GetTaskByIdQuery : IRequest<Response<GetTaskResult>>
    {
        public int Id { get; set; }
        public GetTaskByIdQuery(int id)
        {
            Id = id;
        }
    }
}
