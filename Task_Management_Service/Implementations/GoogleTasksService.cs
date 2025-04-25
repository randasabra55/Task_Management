/*using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class GoogleTasksService : IGoogleTasksService
    {
        private readonly TasksService _tasksService;

        public GoogleTasksService(string clientId, string clientSecret)
        {
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
                new[] { TasksService.Scope.Tasks },
                "user", CancellationToken.None).Result;

            _tasksService = new TasksService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Tasks API Sample",
            });
        }

        // الدالة دي بترجع مهام Google Tasks كـ List من Google.Apis.Tasks.v1.Data.Task
        public async Task<IList<Google.Apis.Tasks.v1.Data.Task>> GetTasksAsync()
        {
            var taskLists = await _tasksService.Tasklists.List().ExecuteAsync();
            var tasks = new List<Google.Apis.Tasks.v1.Data.Task>();

            foreach (var taskList in taskLists.Items)
            {
                var taskListTasks = await _tasksService.Tasks.List(taskList.Id).ExecuteAsync();

                if (taskListTasks?.Items != null)
                {
                    tasks.AddRange(taskListTasks.Items);
                }
            }

            return tasks;
        }
    }
}
*/

/*using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Task_Management_Service.Abstracts;

public class GoogleTasksService : IGoogleTasksService
{
    public async Task<IList<Google.Apis.Tasks.v1.Data.Task>> GetTasksAsync(string accessToken)
    {
        // string googleAccessToken = "ya29.a0AZYkNZhoTnIY1h0Qoxhae-sgxgjmmXsIIWmhiGb_L1s8nrHjhYrYL-ICpCQ-9QKUXnmg1BnQRK4E76qkolfRiBx_Ni8jT4wt979IGsA9Vy80MywUWvVpRBtfcCFaFT6naIPpfh0ipIXywtRpzy9Oe_Nm_M--YzVIzNc2-C-GaCgYKAYESARUSFQHGX2Mi3X4j4twN4liA3bGUA51aog0175";
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var service = new TasksService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Task_Management",
        });

        var taskLists = await service.Tasklists.List().ExecuteAsync();
        var tasks = new List<Google.Apis.Tasks.v1.Data.Task>();

        foreach (var taskList in taskLists.Items)
        {
            var taskListTasks = await service.Tasks.List(taskList.Id).ExecuteAsync();

            if (taskListTasks?.Items != null)
            {
                tasks.AddRange(taskListTasks.Items);
            }
        }

        return tasks;
    }


}*/

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Task_Management_Service.Abstracts;

public class GoogleTasksService : IGoogleTasksService
{
    public async Task<IList<Google.Apis.Tasks.v1.Data.Task>> GetTasksAsync(string accessToken)
    {
        //var credential = GoogleCredential.FromAccessToken("ya29.a0AZYkNZg51AYyNITYUW2_L3258475Zr87ibW0KWIi2WTPSDsX5dd13JXzyt3iCJCAcAhgipdlO8FU6Ji1e-jspTzvOZLuFpvR05D2pQpSA7MxMz7PDQXahy9pjBSCPcYdbXjh1yo2FNp9_ClB2LOVAPPMc9NBbQoDEmD6QoKNaCgYKAcESARUSFQHGX2MiMGHMWagooIVFsPmTact1gg0175");
        var credential = GoogleCredential.FromAccessToken(accessToken);

        var service = new TasksService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Task_Management",
        });

        var taskLists = await service.Tasklists.List().ExecuteAsync();
        var tasks = new List<Google.Apis.Tasks.v1.Data.Task>();

        foreach (var taskList in taskLists.Items)
        {
            var taskListTasks = await service.Tasks.List(taskList.Id).ExecuteAsync();

            if (taskListTasks?.Items != null)
            {
                tasks.AddRange(taskListTasks.Items);
            }
        }

        return tasks;
    }
}


/*using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

public interface IGoogleTasksService
{
    Task<IList<Google.Apis.Tasks.v1.Data.Task>> GetTasksAsync(string accessToken);
    Task<bool> VerifyTokenAsync(string accessToken);
}

public class GoogleTasksService : IGoogleTasksService
{
    private readonly ILogger<GoogleTasksService> _logger;
    private readonly HttpClient _httpClient;

    public GoogleTasksService(ILogger<GoogleTasksService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IList<Google.Apis.Tasks.v1.Data.Task>> GetTasksAsync(string accessToken)
    {
        var accessTokenn = "ya29.a0AZYkNZg51AYyNITYUW2_L3258475Zr87ibW0KWIi2WTPSDsX5dd13JXzyt3iCJCAcAhgipdlO8FU6Ji1e-jspTzvOZLuFpvR05D2pQpSA7MxMz7PDQXahy9pjBSCPcYdbXjh1yo2FNp9_ClB2LOVAPPMc9NBbQoDEmD6QoKNaCgYKAcESARUSFQHGX2MiMGHMWagooIVFsPmTact1gg0175";
        try
        {
            if (!await VerifyTokenAsync(accessTokenn))
            {
                throw new UnauthorizedAccessException("Invalid or expired access token");
            }

            var credential = GoogleCredential.FromAccessToken(accessTokenn);

            var service = new TasksService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Task_Management",
            });

            _logger.LogInformation("Fetching task lists from Google Tasks API");
            var taskLists = await service.Tasklists.List().ExecuteAsync();

            if (taskLists?.Items == null)
            {
                _logger.LogInformation("No task lists found");
                return new List<Google.Apis.Tasks.v1.Data.Task>();
            }

            var allTasks = new List<Google.Apis.Tasks.v1.Data.Task>();

            foreach (var taskList in taskLists.Items)
            {
                try
                {
                    _logger.LogDebug($"Fetching tasks for task list: {taskList.Title} ({taskList.Id})");
                    var tasks = await service.Tasks.List(taskList.Id)
                        .ExecuteAsync()
                        .ConfigureAwait(false);

                    if (tasks?.Items != null)
                    {
                        allTasks.AddRange(tasks.Items);
                        _logger.LogDebug($"Found {tasks.Items.Count} tasks in list {taskList.Title}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error fetching tasks for task list {taskList.Title}");
                    // Continue with next task list
                }
            }

            return allTasks;
        }
        catch (Google.GoogleApiException ex) when (ex.HttpStatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogError(ex, "Google API authorization failed");
            throw new UnauthorizedAccessException("Google API authorization failed. Token may be invalid or expired.", ex);
        }
        catch (Google.GoogleApiException ex)
        {
            _logger.LogError(ex, $"Google API error: {ex.Error?.Message}");
            throw new ApplicationException($"Google API error: {ex.Error?.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching Google tasks");
            throw;
        }
    }

    public async Task<bool> VerifyTokenAsync(string accessToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                _logger.LogWarning("Empty access token provided");
                return false;
            }

            var response = await _httpClient.GetAsync(
                $"https://oauth2.googleapis.com/tokeninfo?access_token={accessToken}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Token verification failed with status: {response.StatusCode}");
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var tokenInfo = JsonSerializer.Deserialize<TokenInfo>(content);

            if (tokenInfo == null || string.IsNullOrEmpty(tokenInfo.Scope))
            {
                _logger.LogWarning("Invalid token info response");
                return false;
            }

            // Verify required scope is present
            if (!tokenInfo.Scope.Contains("https://www.googleapis.com/auth/tasks") &&
                !tokenInfo.Scope.Contains("https://www.googleapis.com/auth/tasks.readonly"))
            {
                _logger.LogWarning("Token missing required Tasks API scope");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying Google token");
            return false;
        }
    }

    private class TokenInfo
    {
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}

// Extension for DI registration
public static class GoogleTasksServiceExtensions
{
    public static IServiceCollection AddGoogleTasksService(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IGoogleTasksService, GoogleTasksService>();
        return services;
    }
}

*/
