/*using Microsoft.Extensions.Hosting;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class SyncGoogleTasksBackgroundService : BackgroundService
    {
        private readonly ISyncGoogleTasksService _syncGoogleTasksService;

        public SyncGoogleTasksBackgroundService(ISyncGoogleTasksService syncGoogleTasksService)
        {
            _syncGoogleTasksService = syncGoogleTasksService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _syncGoogleTasksService.SyncTasksAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error syncing tasks: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); 
            }
        }
    }
}
*/



using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Task_Management_Service.Abstracts;

public class GoogleTasksBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<GoogleTasksService> _logger;


    public GoogleTasksBackgroundService(IServiceScopeFactory scopeFactory, ILogger<GoogleTasksService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var syncService = scope.ServiceProvider.GetRequiredService<ISyncGoogleTasksService>();

            try
            {
                await syncService.SyncTasksAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error syncing Google tasks: {ex.Message}");
                _logger.LogError(ex, "Error occurred while fetching tasks from Google.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}
