/*using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Data;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class GoogleTasksSyncService : ISyncGoogleTasksService
    {
        private readonly IGoogleTasksService _googleTasksService;
        private readonly Context _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoogleTasksSyncService(
            IGoogleTasksService googleTasksService,
            Context dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _googleTasksService = googleTasksService;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SyncTasksAsync(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return;

            var tasks = await _googleTasksService.GetTasksAsync();

            foreach (var task in tasks)
            {
                var existingTask = _dbContext.externalAPITasks
                    .FirstOrDefault(t => t.TaskId == task.Id.GetHashCode() && t.UserId == userId);

                if (existingTask == null)
                {
                    var externalTask = new ExternalAPITasks
                    {
                        Title = task.Title,
                        Description = task.Notes,
                        ExternalSource = "Google Tasks",
                        //DueDate=task.Due,
                        // DueDate = task.Due ,//?? DateTime.Now,
                        UserId = userId,
                        TaskId = task.Id.GetHashCode()

                    };

                    _dbContext.externalAPITasks.Add(externalTask);
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            // await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // التكرار كل 10 دقائق

        }
    }
}
*/

using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Data;
using Task_Management_Service.Abstracts;

public class GoogleTasksSyncService : ISyncGoogleTasksService
{
    private readonly IGoogleTasksService _googleTasksService;
    private readonly Context _dbContext;

    public GoogleTasksSyncService(
        IGoogleTasksService googleTasksService,
        Context dbContext)
    {
        _googleTasksService = googleTasksService;
        _dbContext = dbContext;
    }
    public async Task SyncTasksAsync(CancellationToken cancellationToken)
    {
        try
        {
            var users = _dbContext.Users
                .Where(u => u.GoogleAccessToken != null)
                .ToList();

            foreach (var user in users)
            {
                var tasks = await _googleTasksService.GetTasksAsync(user.GoogleAccessToken);

                foreach (var task in tasks)
                {
                    var existingTask = _dbContext.externalAPITasks
                        .FirstOrDefault(t => t.ExternalTaskId == task.Id && t.UserId == user.Id);

                    if (existingTask == null)
                    {
                        var externalTask = new ExternalAPITasks
                        {
                            Title = task.Title ?? "No title",
                            Description = task.Notes,
                            ExternalSource = "Google Tasks",
                            UserId = user.Id,
                            ExternalTaskId = task.Id
                        };

                        _dbContext.externalAPITasks.Add(externalTask);
                    }
                }
            }

            var changes = await _dbContext.SaveChangesAsync(cancellationToken);
            Console.WriteLine($"Saved {changes} changes to database");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error syncing tasks: {ex.Message}");
            throw; // Re-throw to see the error in your logs
        }
    }
    /* public async Task SyncTasksAsync(CancellationToken cancellationToken)
     {
         var users = _dbContext.Users
             .Where(u => u.GoogleAccessToken != null)
             .ToList();

         foreach (var user in users)
         {
             var tasks = await _googleTasksService.GetTasksAsync(user.GoogleAccessToken);

             foreach (var task in tasks)
             {
                 var existingTask = _dbContext.externalAPITasks
                     .FirstOrDefault(t => t.ExternalTaskId == task.Id && t.UserId == user.Id);

                 if (existingTask == null)
                 {
                     var externalTask = new ExternalAPITasks
                     {
                         Title = task.Title,
                         Description = task.Notes,
                         ExternalSource = "Google Tasks",
                         // DueDate = task.Due.To,
                         UserId = user.Id,
                         ExternalTaskId = task.Id
                     };

                     _dbContext.externalAPITasks.Add(externalTask);
                 }
             }
         }

         await _dbContext.SaveChangesAsync(cancellationToken);
     }*/
}
