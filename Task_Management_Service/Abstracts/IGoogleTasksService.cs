namespace Task_Management_Service.Abstracts
{
    public interface IGoogleTasksService
    {
        Task<IList<Google.Apis.Tasks.v1.Data.Task>> GetTasksAsync(string accessToken);
    }
}