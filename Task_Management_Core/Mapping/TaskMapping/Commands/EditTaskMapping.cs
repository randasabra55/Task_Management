using Task_Management_Core.Features.Taskkss.Commands.Models;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.TaskMapping
{
    public partial class TaskProfile
    {
        public void EditTaskMapping()
        {
            CreateMap<EditTaskCommand, Taskss>();
        }
    }
}
