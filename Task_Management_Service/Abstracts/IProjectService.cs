using Task_Management_Data.Entities;

namespace Task_Management_Service.Abstracts
{
    public interface IProjectService
    {
        public Task<string> addProject(Projects project);
        public Task<string> EditProject(Projects project);
        public Task<string> DeleteProject(int id);
        public Task<Projects> GetProjectById(int id);
        public IQueryable<Projects> GetProjectsPaginated();



    }
}
