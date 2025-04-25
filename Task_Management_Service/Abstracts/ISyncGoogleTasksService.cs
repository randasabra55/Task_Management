namespace Task_Management_Service.Abstracts
{
    public interface ISyncGoogleTasksService
    {
        public Task SyncTasksAsync(CancellationToken cancellationToken);
    }
}
