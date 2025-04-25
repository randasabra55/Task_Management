using AutoMapper;

namespace Task_Management_Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            EditUserMapping();
            GetUserById();
            GetUserPaginatedListMapping();
        }
    }
}
