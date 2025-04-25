using Task_Management_Data.Entities;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Abstracts
{
    public interface ITaskRepository : IGenericRepository<Taskss>
    {
        public IQueryable<Taskss> GetTaskssesQuerable();
        public List<Taskss> GetAllTasksForSpecificProject(int projectId);
        public List<Taskss> GetAllTasksForUser(string userId);

    }
}
