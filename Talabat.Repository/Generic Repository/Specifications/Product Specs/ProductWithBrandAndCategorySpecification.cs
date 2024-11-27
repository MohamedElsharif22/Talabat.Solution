using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repositories.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecification : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecification(ProductSpecParams specParams)
            : base(P =>
                       (string.IsNullOrWhiteSpace(specParams.Search) || EF.Functions.Like(P.Name, $"%{specParams.Search}%"))
                        &&
                       (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId)
                        &&
                       (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId)
                  )
        {

            if (!specParams.GetCountOnly)
            {
                Includes.Add(P => P.Brand);
                Includes.Add(P => P.Category);

                if (!string.IsNullOrWhiteSpace(specParams.Sort))
                {
                    switch (specParams.Sort)
                    {
                        case "priceAsc":
                            AddOrderByAsc(P => P.Price);
                            break;
                        case "priceDesc":
                            AddOrderByDesc(P => P.Price);
                            break;
                        default:
                            AddOrderByAsc(P => P.Name);
                            break;
                    }
                }
                else { AddOrderByAsc(P => P.Name); }

                ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
            }

        }
        public ProductWithBrandAndCategorySpecification(Expression<Func<Product, bool>> criteria) : base(criteria)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }


    }
}
