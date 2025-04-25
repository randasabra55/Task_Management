using Microsoft.EntityFrameworkCore;
using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Infrastructure.Data;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Implementations
{
    public class TaskRepository : GenericRepository<Taskss>, ITaskRepository
    {
        private DbSet<Taskss> tasksses;

        public TaskRepository(Context dbContext) : base(dbContext)
        {
            tasksses = dbContext.Set<Taskss>();
        }

        public IQueryable<Taskss> GetTaskssesQuerable()
        {
            return tasksses.AsNoTracking().AsQueryable();
        }
    }
}
