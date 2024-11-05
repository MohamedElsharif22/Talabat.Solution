using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class CategoryProductsResolver : IValueResolver<Category, CategoryToReturnDTO, ICollection<RelatedProductDto>>
    {
        public ICollection<RelatedProductDto> Resolve(Category source, CategoryToReturnDTO destination, ICollection<RelatedProductDto> destMember, ResolutionContext context)
        {
            if (source.Products.Count != 0)
            {
                return source.Products.Select(P => new RelatedProductDto { Id = P.Id, Name = P.Name }).ToHashSet();
            }
            return destMember;
        }
    }
}
