using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.AccountDTOs
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
