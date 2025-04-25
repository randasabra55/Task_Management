using Microsoft.EntityFrameworkCore;
using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Data;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class NotificationService : INotificationService
    {
        Context context;
        public NotificationService(Context context)
        {
            this.context = context;
        }
        public async Task<string> AddNotification(Notifications n)
        {
            await context.notifications.AddAsync(n);
            await context.SaveChangesAsync();
            return "Success";
        }

        public async Task<Notifications> GetNotificationById(int id)
        {
            var notify = await context.notifications.FirstOrDefaultAsync(n => n.Id == id);
            notify.IsRead = true;
            context.notifications.Update(notify);
            context.SaveChanges();
            return notify;
        }
        public async Task<List<Notifications>> GetAllNotificationForUser(string userId)
        {
            return await context.notifications.Where(n => n.UserId == userId).ToListAsync();

        }
    }
}
