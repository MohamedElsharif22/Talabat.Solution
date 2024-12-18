﻿namespace Talabat.APIs.DTOs.ProductDTOs
{
    public record ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Brand { get; set; } = null!;
        public int BrandId { get; set; }
    }
}