using Task_Management_Core.Features.Projectss.Commands.Models;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.ProjectMapping
{
    public partial class ProjectProfile
    {
        public void AddProjectMapping()
        {
            CreateMap<AddProjectCommand, Projects>();
        }
    }
}
