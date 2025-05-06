using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.ProductDTOs
{
    public class ProductRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Image")]
        public IFormFile ProductPicture { get; set; } = null!;
        [Required]
        [Range(1.0, 1_000_000, ErrorMessage = "Price must be between 1.00 and 1,000,000")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}
