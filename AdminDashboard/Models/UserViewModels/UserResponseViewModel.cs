using System.ComponentModel;

namespace AdminDashboard.MVC.Models.UserViewModels
{
    public class UserResponseViewModel(IList<string>? roles)
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required AddressRequestViewModel Address { get; set; }
        public string? PhoneNumber { get; set; }
        public IList<string>? Roles { get; set; } = roles;

        [DisplayName("Roles")]
        public string? StringRoles { get; set; } = roles?.Aggregate((current, next) => $"{current},{next}");


    }
}
