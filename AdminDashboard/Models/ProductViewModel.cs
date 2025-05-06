using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.MVC.Models
{
    public class ProductViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(10)]
        public string Description { get; set; } = null!;
        [Required]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}
