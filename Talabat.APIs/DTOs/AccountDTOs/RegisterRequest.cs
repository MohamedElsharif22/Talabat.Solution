using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.AccountDTOs
{
    public record RegisterRequest
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [Required]
        [Phone]
        public string Phone { get; set; } = null!;
        [Required]
        [RegularExpression(@"(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()+}{"":;'?/><,])(?!.*\s).*",
            ErrorMessage = "Password must have at least 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; } = null!;
    }
}
