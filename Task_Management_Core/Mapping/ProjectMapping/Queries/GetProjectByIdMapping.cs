using Task_Management_Core.Features.Projectss.Queries.Results;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.ProjectMapping
{
    public partial class ProjectProfile
    {
        public void GetProjectByIdMapping()
        {
            CreateMap<Projects, GetProjectResult>();
        }
    }
}
