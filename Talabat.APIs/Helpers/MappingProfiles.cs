using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToGetDto>()
                .ForMember(dest => dest.Brand, O => O.MapFrom(source => source.Brand.Name))
                .ForMember(dest => dest.Category, O => O.MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Brand, BrandToReturnDto>()
                .ForMember(D => D.Products, O => O.MapFrom<BrandProductsResolver>());

            CreateMap<Category, CategoryToReturnDTO>()
                .ForMember(D => D.Products, O => O.MapFrom<CategoryProductsResolver>());
        }

    }
}
