
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



