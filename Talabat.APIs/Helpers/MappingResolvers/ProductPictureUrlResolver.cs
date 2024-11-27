using AutoMapper;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers.MappingResolvers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToGetDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToGetDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
                return $"{_configuration["BaseApiUrl"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
