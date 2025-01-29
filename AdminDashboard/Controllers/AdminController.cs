using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Identity;

namespace AdminDashboard.Controllers
{

    public class AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                ModelState.AddModelError("Email", "Invalid Email!");
                return View(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Incorrect Password!");

                return View(model);
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
            {
                ModelState.AddModelError("Authorized", "you are not authorized !");
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");


        }
        #endregion


        #region SignOut
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

    }
}
