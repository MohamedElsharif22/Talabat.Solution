namespace Talabat.APIs.DTOs.AccountDTOs
{
    public record UserResponse
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
