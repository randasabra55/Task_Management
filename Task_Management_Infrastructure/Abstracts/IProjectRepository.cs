using Task_Management_Data.Entities;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure.Abstracts
{
    public interface IProjectRepository : IGenericRepository<Projects>
    {
        public IQueryable<Projects> GetProjectsQuerable();
    }
}
