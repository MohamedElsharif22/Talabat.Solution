using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repositories._Identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Mohamed Kamal",
                    UserName = "MohamedElsharif",
                    Email = "mohamed.elsharif022@gmail.com",
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
