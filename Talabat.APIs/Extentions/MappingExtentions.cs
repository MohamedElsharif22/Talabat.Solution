using Microsoft.AspNetCore.Identity;
using Talabat.APIs.DTOs.AccountDTOs;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contracts;

namespace Talabat.APIs.Extentions
{
    public static class MappingExtentions
    {
        public static async Task<UserDTO> ToUserDTOAsync(this ApplicationUser user, IAuthService _authService, UserManager<ApplicationUser> _userManager)
        {
            return new UserDTO
            {
                Name = user.DisplayName,
                Email = user.Email!,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            };
        }
    }
}
