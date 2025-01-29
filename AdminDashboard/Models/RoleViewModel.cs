using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public record RoleViewModel
    {
        public string? Id { get; set; }

        [Required]
        [Length(3, 20, ErrorMessage = "Role name Must be at least 3 characters and maximum 20!")]
        public string Name { get; set; } = string.Empty;
    }
}
