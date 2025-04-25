
using Microsoft.AspNetCore.Identity;

namespace Task_Management_Data.Entities.Identity
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string? GoogleAccessToken { get; set; }




        //public virtual ICollection<ProjectMembers> ProjectMembers { get; set; }
        public virtual ICollection<Taskss> Tasksses { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual ICollection<ExternalAPITasks> ExternalAPITasks { get; set; }

    }
}



