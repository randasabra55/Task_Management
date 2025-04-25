


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Task_Management_Infrastructure.Seeder
{
    public static class RoleSeeder
    {

        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleCount = await roleManager.Roles.CountAsync();
            if (roleCount <= 0)
            {
                //await userManager.AddToRoleAsync(User, "User");
                await roleManager.CreateAsync(new IdentityRole() { Name = "Manager" });
                await roleManager.CreateAsync(new IdentityRole() { Name = "Employee" });
                //await roleManager.CreateAsync(new Role() { Name = "Admin" });
            }
        }

    }
}
