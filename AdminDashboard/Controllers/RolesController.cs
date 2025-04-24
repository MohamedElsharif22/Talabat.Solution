using AdminDashboard.MVC.Helpers.Mapping;
using AdminDashboard.MVC.Models;
using AdminDashboard.MVC.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace AdminDashboard.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<IActionResult> Index(string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                var filterdRoles = await _roleManager.Roles.Where(R => EF.Functions.Like(R.Name, $"%{search}%")).Select(R => R.ToRoleViewModel()).ToListAsync();
                return View(filterdRoles);
            }
            var roles = await _roleManager.Roles.Select(R => R.ToRoleViewModel()).ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(model.Name);
                if (roleExists)
                {
                    ModelState.AddModelError(string.Empty, "Role is already exist!");
                    return View(model);
                }
                var result = await _roleManager.CreateAsync(model.ToIdentityRole());
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is not null)
                {
                    TempData["id"] = role.Id;
                    return View(role.ToRoleViewModel());
                }
                return NotFound();
            }
            return View("Index", id);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id ?? "");
                if (role is not null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
                ModelState.AddModelError(string.Empty, "Unable to update role");
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return BadRequest();

            return View(role.ToRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (model.Id is null)
            {
                ModelState.AddModelError("id", "id can't be null here");
                return View(model);
            }

            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role is null) return BadRequest();

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return RedirectToAction(nameof(Index));

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, string? userName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null) return RedirectToAction(nameof(Index));

            ViewData["roleId"] = roleId;
            var usersInRoleVM = new List<UserInRoleViewModel>();
            // Search for specific users
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var filterdUsers = await _userManager.Users.Where(U => EF.Functions.Like(U.UserName, $"%{userName}%")).ToListAsync();
                foreach (var user in filterdUsers)
                {
                    usersInRoleVM.Add(new UserInRoleViewModel()
                    {
                        Id = user.Id,
                        Name = user.UserName,
                        IsInRole = await _userManager.IsInRoleAsync(user, role.Name),
                    });
                }
                return View(usersInRoleVM);

            }
            // Get all users
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                usersInRoleVM.Add(new UserInRoleViewModel()
                {
                    Id = user.Id,
                    Name = user.UserName,
                    IsInRole = await _userManager.IsInRoleAsync(user, role.Name),
                });
            }
            return View(usersInRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string? roleId, IEnumerable<UserInRoleViewModel> usersInRole)
        {

            if (roleId is null) return View();

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null) return View();

            foreach (var userInRole in usersInRole)
            {
                var user = await _userManager.FindByIdAsync(userInRole.Id ?? "");
                if (user is null) continue;

                var inRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (!inRole && userInRole.IsInRole)
                {
                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded) continue;
                    }
                    ModelState.AddModelError($"UserId: {user.Id}", "Error occured while adding user to role!");
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (inRole && !userInRole.IsInRole)
                {
                    if (user.Id == currentUserId && role.Name.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError($"UserId: {user.Id}", "You can't remove yourself from the admin role!");
                        return View(usersInRole);
                    }
                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded) continue;
                    }
                    ModelState.AddModelError($"UserId: {user.Id}", "Error occured while removing user From role!");
                    return View(usersInRole);
                }
            }


            return RedirectToAction("Index");
        }


    }
}
