namespace Talabat.APIs.DTOs.ProductDTOs
{
    public record BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
