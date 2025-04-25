using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Projectss.Queries.Results;

namespace Task_Management_Core.Features.Projectss.Queries.Models
{
    public class GetProjectByIdQuery : IRequest<Response<GetProjectResult>>
    {
        public int Id { get; set; }
        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}
