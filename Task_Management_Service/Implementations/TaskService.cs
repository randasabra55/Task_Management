using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class TaskService : ITaskService
    {
        ITaskRepository taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
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

        public IQueryable<Taskss> GetTasksPaginated()
        {
            return taskRepository.GetTaskssesQuerable();
        }
    }
}
