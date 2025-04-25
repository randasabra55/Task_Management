using Task_Management_Core.Features.Filess.Queries.Results;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.FileMapping
{
    public partial class FileProfile
    {
        public void GetFileByIdMapping()
        {
            CreateMap<Files, GetFileResult>();
        }
    }
}
