using AutoMapper;
using Talabat.APIs.DTOs.AccountDTOs;
using Talabat.APIs.DTOs.BasketDTOs;
using Talabat.APIs.DTOs.OrderDTOs;
using Talabat.APIs.DTOs.ProductDTOs;
using Talabat.APIs.Helpers.MappingResolvers;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Basket;
using Talabat.Core.Entities.Order_Aggregate;
using IdentityAddress = Talabat.Core.Entities.Identity.Address;
using OrderAddress = Talabat.Core.Entities.Order_Aggregate.Address;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Brand, O => O.MapFrom(source => source.Brand.Name))
                .ForMember(dest => dest.Category, O => O.MapFrom(source => source.Category.Name))
                .ForMember(dest => dest.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Brand, BrandResponse>();

            CreateMap<Category, CategoryResponse>();

            CreateMap<BasketItem, BasketItemRequest>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketRequest>().ReverseMap();
            CreateMap<IdentityAddress, AddressDTO>().ReverseMap();

            CreateMap<AddressRequest, OrderAddress>();

            CreateMap<Order, OrderResponse>();
            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }



    }
}
