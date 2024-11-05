namespace Talabat.APIs.DTOs
{
    public class CategoryToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RelatedProductDto> Products { get; set; } = new HashSet<RelatedProductDto>();
    }
}
