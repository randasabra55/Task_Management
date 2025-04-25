using Task_Management_Data.Entities;

namespace Task_Management_Service.Abstracts
{
    public interface INotificationService
    {
        public Task<string> AddNotification(Notifications n);
        public Task<Notifications> GetNotificationById(int id);
        public Task<List<Notifications>> GetAllNotificationForUser(string userId);
    }
}
