using Task_Management_Core.Features.Filess.Commands.Models;
using Task_Management_Data.Entities;

namespace Task_Management_Core.Mapping.FileMapping
{
    public partial class FileProfile
    {
        public void EditFileMapping()
        {
            CreateMap<EditFileCommand, Files>();
        }
    }
}
