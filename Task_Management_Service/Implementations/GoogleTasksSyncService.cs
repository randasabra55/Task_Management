
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
            throw;
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
