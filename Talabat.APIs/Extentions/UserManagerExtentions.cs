using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<ApplicationUser?> FindUserWithAddressByEmail(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            var appUser = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.NormalizedEmail == email.ToUpper());

            return appUser;
        }
    }
}
