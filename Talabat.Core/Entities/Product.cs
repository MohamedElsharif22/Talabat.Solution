﻿namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public Brand Brand { get; set; } = null!;
        public int BrandId { get; set; }
    }
}
