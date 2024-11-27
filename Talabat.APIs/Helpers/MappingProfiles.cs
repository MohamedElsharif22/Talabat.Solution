using AutoMapper;
using Talabat.APIs.DTOs.AccountDTOs;
using Talabat.APIs.DTOs.BasketDTOs;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.APIs.Helpers.MappingResolvers;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Identity;

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

            CreateMap<Brand, BrandToReturnDto>();

            CreateMap<Category, CategoryToReturnDTO>();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }



    }
}
