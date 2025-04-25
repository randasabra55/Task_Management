using AutoMapper;


namespace Task_Management_Core.Mapping.NotificationMapping
{
    public partial class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            GetNotificationByIdMapping();
            GetNotificationListMapping();
        }
    }
}
