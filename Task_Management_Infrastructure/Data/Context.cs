
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task_Management_Data.Entities;
using Task_Management_Data.Entities.Identity;


namespace Task_Management_Infrastructure.Data
{
    public class Context : IdentityDbContext<User>
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Files> files { get; set; }
        public DbSet<ExternalAPITasks> externalAPITasks { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        //public DbSet<ProjectMembers> projectMembers { get; set; }
        public DbSet<Projects> projects { get; set; }
        public DbSet<Reviews> reviews { get; set; }
        public DbSet<Taskss> tasksses { get; set; }

    }

}
