using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models.UserViewModels
{
    public class UserRequestViewModel
    {
        public string Id { get; set; }
        public required string UserName { get; set; }
        [Required]
        public required string DisplayName { get; set; }
        [Required(ErrorMessage = "Email is Requierd!")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required AddressRequestViewModel Address { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }

        public IEnumerable<UserInRoleViewModel> Roles { get; set; }

    }
}
