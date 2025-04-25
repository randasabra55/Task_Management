using AutoMapper;

namespace Task_Management_Core.Mapping.ProjectMapping
{
    public partial class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            AddProjectMapping();
            EditProjectMapping();
            GetProjectByIdMapping();
            GetProjectsPaginatedMapping();
        }
    }
}
