using AdminDashboard.MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace AdminDashboard.MVC.Helpers.Mapping
{
    public static class RoleMapping
    {
        public static IdentityRole ToIdentityRole(this RoleViewModel model)
        {
            return new IdentityRole(model.Name);
        }
        public static RoleViewModel ToRoleViewModel(this IdentityRole model)
        {
            return new RoleViewModel() { Id = model.Id, Name = model.Name };
        }
    }
}
