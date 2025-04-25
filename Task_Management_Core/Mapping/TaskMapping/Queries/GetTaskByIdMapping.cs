using Task_Management_Core.Features.Taskkss.Queries.Results;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.TaskMapping
{
    public partial class TaskProfile
    {
        public void GetTaskByIdMapping()
        {
            CreateMap<Taskss, GetTaskResult>();
        }
    }
}
