using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Filess.Queries.Results;

namespace Task_Management_Core.Features.Filess.Queries.Models
{
    public class GetFileByIdQuery : IRequest<Response<GetFileResult>>
    {
        public int Id { get; set; }
        public GetFileByIdQuery(int id)
        {
            Id = id;
        }
    }
}
