using Task_Management_Core.Features.Authentications.Commands.Models;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
