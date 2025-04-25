using Task_Management_Core.Features.Authentications.Queries.Results;
using Task_Management_Data.Entities.Identity;

namespace Task_Management_Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void GetUserById()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
