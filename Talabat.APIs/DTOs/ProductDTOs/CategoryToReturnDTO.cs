namespace Talabat.APIs.DTOs.ProductDTOs
{
    public class CategoryToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<RelatedProductDto> Products { get; set; } = new HashSet<RelatedProductDto>();
    }
}
