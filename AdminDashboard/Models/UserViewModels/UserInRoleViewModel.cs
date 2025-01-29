namespace AdminDashboard.Models.UserViewModels
{
    public class UserInRoleViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool IsInRole { get; set; } = false;
    }
}
