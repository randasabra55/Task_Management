using Task_Management_Core.Features.Taskkss.Queries.Results;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.TaskMapping
{
    public partial class TaskProfile
    {
        public void GetTasksForProjectMapping()
        {

            CreateMap<Files, Filee>()
                .ForMember(dest => dest.FileUrl, opt => opt.MapFrom(src => src.FileURL))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName));

            CreateMap<Taskss, GetTasksForSpecificProjectResult>()
               .ForMember(dest => dest.files, opt => opt.MapFrom(src => src.Files));
        }
    }
}
