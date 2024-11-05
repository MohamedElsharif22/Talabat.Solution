using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecification : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecification()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
        public ProductWithBrandAndCategorySpecification(Expression<Func<Product, bool>> criteria) : base(criteria)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }


    }
}
