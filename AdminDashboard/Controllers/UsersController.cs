using AdminDashboard.Helpers.Mapping;
using AdminDashboard.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;

namespace AdminDashboard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var mappedUsers = new List<UserResponseViewModel>();


            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                mappedUsers.Add(user.ToUserResponseViewModel(userRoles));
            }

            return View(mappedUsers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "details")
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Id == id);

            if (user is null) return BadRequest();

            var userRoles = await _userManager.GetRolesAsync(user);

            if (viewName == nameof(Edit))
            {
                var allRoles = await _roleManager.Roles.ToListAsync();
                List<UserInRoleViewModel> userInRoleViewModels = new List<UserInRoleViewModel>();

                foreach (var role in allRoles)
                {

                    userInRoleViewModels.Add(new UserInRoleViewModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                        IsInRole = userRoles.Contains(role.Name ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                    });
                }
                return View(viewName, user.ToUserRequestViewModel(userInRoleViewModels));
            }

            return View(viewName, user.ToUserResponseViewModel(userRoles));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, nameof(Edit));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserRequestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Id == model.Id);

            if (user is null) return RedirectToAction(nameof(Index));

            user = model.ToAppUser(user);

            if (model.Roles.Any())
            {
                foreach (var userRole in model.Roles)
                {
                    userRole.Name ??= "";

                    var result = await _userManager.IsInRoleAsync(user, userRole.Name);

                    if (userRole.IsInRole && !result)
                        await _userManager.AddToRoleAsync(user, userRole.Name);
                    else if (!userRole.IsInRole && result)
                        await _userManager.RemoveFromRoleAsync(user, userRole.Name);

                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var err in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return View(model);
        }
    }
}
