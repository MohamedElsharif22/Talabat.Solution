using AdminDashboard.Models;
using Microsoft.AspNetCore.Identity;

namespace AdminDashboard.Helpers.Mapping
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
