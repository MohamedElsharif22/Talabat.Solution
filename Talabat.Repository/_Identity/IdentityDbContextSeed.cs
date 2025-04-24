using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Application._Identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                if (!roleManager.Roles.Any())
                {
                    var adminRole = new IdentityRole("Admin");

                    await roleManager.CreateAsync(adminRole);
                }

                var user = new ApplicationUser()
                {
                    DisplayName = "Muhammad Kamal",
                    UserName = "admin_seed",
                    Email = "admin@aspnet.com",

                };

                await userManager.CreateAsync(user, "P@ssw0rd");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
