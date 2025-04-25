

namespace Task_Management_Infrastructure.Seeder
{
    public static class UserSeeder
    {
        /*public static async Task SeedAsync(UserManager<User> userManager)
        {
            var userCount = await userManager.Users.CountAsync();
            if (userCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FullName = "admin",
                    Role = "Admin",
                    // PhoneNumber = "123456",
                    // Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var result = await userManager.CreateAsync(defaultuser, "P@$$w0rd");
                *//*defaultuser.Role = "Admin";*//*
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultuser, "Admin");
                }

            }*/

        //}
    }
}
