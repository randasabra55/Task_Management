using AutoMapper;

namespace Task_Management_Core.Mapping.TaskMapping
{
    public partial class TaskProfile : Profile
    {
        public TaskProfile()
        {
            AddTaskMapping();
            EditTaskMapping();
            GetTaskByIdMapping();
            GetTaskPaginatedMapping();
        }
    }
}
