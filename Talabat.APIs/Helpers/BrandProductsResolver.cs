using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class BrandProductsResolver : IValueResolver<Brand, BrandToReturnDto, ICollection<RelatedProductDto>>
    {
        public ICollection<RelatedProductDto> Resolve(Brand source, BrandToReturnDto destination, ICollection<RelatedProductDto> destMember, ResolutionContext context)
        {
            if (source.Products.Count != 0)
            {
                return source.Products.Select(P => new RelatedProductDto { Id = P.Id, Name = P.Name }).ToHashSet();
            }
            return destMember;
        }
    }
}
