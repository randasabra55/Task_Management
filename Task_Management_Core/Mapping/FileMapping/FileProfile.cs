using AutoMapper;

namespace Task_Management_Core.Mapping.FileMapping
{
    public partial class FileProfile : Profile
    {
        public FileProfile()
        {
            AddFileMapping();
            EditFileMapping();
            GetFileByIdMapping();
            GetFilesPaginatedMapping();
            AddFileToGoogleMapping();
            EditFileOnGoogleMapping();
        }
    }
}

