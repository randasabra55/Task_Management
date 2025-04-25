using MediatR;
using Task_Management_Core.Bases;
using Task_Management_Core.Features.Notificationss.Queries.Results;

namespace Task_Management_Core.Features.Notificationss.Queries.Models
{
    public class GetNotificationListQuery : IRequest<Response<List<GetNotificationResult>>>
    {
        public string UserId { get; set; }
        public GetNotificationListQuery(string id)
        {
            UserId = id;
        }
    }
}
