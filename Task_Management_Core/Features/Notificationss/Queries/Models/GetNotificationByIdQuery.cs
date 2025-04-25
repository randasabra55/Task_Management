using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Notificationss.Queries.Results;

namespace Task_Management_Core.Features.Notificationss.Queries.Models
{
    public class GetNotificationByIdQuery : IRequest<Response<GetNotificationResult>>
    {
        public int Id { get; set; }
        public GetNotificationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
