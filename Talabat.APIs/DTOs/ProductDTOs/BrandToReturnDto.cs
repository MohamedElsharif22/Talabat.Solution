namespace Talabat.APIs.DTOs.ProductDTOs
{
    public class BrandToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<ProductToGetDto> Products { get; set; } = new HashSet<ProductToGetDto>();
    }
}
