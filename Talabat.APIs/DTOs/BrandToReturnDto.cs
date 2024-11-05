namespace Talabat.APIs.DTOs
{
    public class BrandToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RelatedProductDto> Products { get; set; } = new HashSet<RelatedProductDto>();
    }
}
