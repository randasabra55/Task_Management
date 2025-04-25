using Task_Management_Data.Entities;

namespace Task_Management_Service.Abstracts
{
    public interface ITaskService
    {
        public Task<string> addTaskAsync(Taskss task);
        public Task<string> EditTaskAsync(Taskss task);
        public Task<string> DeleteTaskAsync(int id);
        public Task<Taskss> GetTaskByIdAsync(int id);
        public IQueryable<Taskss> GetTasksPaginated();
        public List<Taskss> GetTasksForSpecificProject(int projectId);
        public List<Taskss> GetTasksForUser(string userId);
    }
}
