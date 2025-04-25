using Task_Management_Core.Features.Notificationss.Queries.Results;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.NotificationMapping
{
    public partial class NotificationProfile
    {
        public void GetNotificationListMapping()
        {
            CreateMap<Notifications, GetNotificationResult>();
        }
    }
}
