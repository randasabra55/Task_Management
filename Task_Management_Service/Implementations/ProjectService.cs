using Task_Management_Data.Entities;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Service.Abstracts;

namespace Task_Management_Service.Implementations
{
    public class ProjectService : IProjectService
    {
        IProjectRepository projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<string> addProject(Projects project)
        {
            await projectRepository.AddAsync(project);
            return "Success";
        }

        public async Task<string> DeleteProject(int id)
        {
            var proj = await projectRepository.GetByIdAsync(id);
            if (proj == null) return "NotFound";
            await projectRepository.DeleteAsync(proj);
            return "Success";
        }

        public async Task<string> EditProject(Projects project)
        {
            await projectRepository.UpdateAsync(project);
            return "Success";
        }

        public async Task<Projects> GetProjectById(int id)
        {
            return await projectRepository.GetByIdAsync(id);
        }

        public IQueryable<Projects> GetProjectsPaginated()
        {
            return projectRepository.GetProjectsQuerable();
        }
    }
}
