namespace Talabat.APIs.DTOs.ProductDTOs
{
    public record CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
