using Microsoft.EntityFrameworkCore;
using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Infrastructure.Data;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Implementations
{
    public class ProjectRepository : GenericRepository<Projects>, IProjectRepository
    {
        private DbSet<Projects> projects;
        public ProjectRepository(Context dbContext) : base(dbContext)
        {
            projects = dbContext.Set<Projects>();
        }

        public IQueryable<Projects> GetProjectsQuerable()
        {
            var list = projects.AsNoTracking().AsQueryable();
            return list;
            //throw new NotImplementedException();
        }
    }
}
