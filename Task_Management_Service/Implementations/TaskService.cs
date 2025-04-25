using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class TaskService : ITaskService
    {
        ITaskRepository taskRepository;
        IProjectRepository projectRepository;
        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            this.taskRepository = taskRepository;
            this.projectRepository = projectRepository;
        }

        public async Task<string> addTaskAsync(Taskss task)
        {
            await taskRepository.AddAsync(task);
            return "Success";
        }

        public async Task<string> DeleteTaskAsync(int id)
        {
            var task = await taskRepository.GetByIdAsync(id);
            if (task == null) return "NotFound";
            await taskRepository.DeleteAsync(task);
            return "Success";
        }

        public async Task<string> EditTaskAsync(Taskss task)
        {
            await taskRepository.UpdateAsync(task);
            return "Success";
        }

        public async Task<Taskss> GetTaskByIdAsync(int id)
        {
            return await taskRepository.GetByIdAsync(id);
        }

        public List<Taskss> GetTasksForSpecificProject(int projectId)
        {
            return taskRepository.GetAllTasksForSpecificProject(projectId);
        }

        public List<Taskss> GetTasksForUser(string userId)
        {
            return taskRepository.GetAllTasksForUser(userId);
        }

        public IQueryable<Taskss> GetTasksPaginated()
        {
            return taskRepository.GetTaskssesQuerable();
        }
    }
}
