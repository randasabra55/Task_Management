using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Authentications.Queries.Results;

namespace Task_Management_Core.Features.Authentications.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        /*public string Id { get; set; }
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }*/
    }
}
