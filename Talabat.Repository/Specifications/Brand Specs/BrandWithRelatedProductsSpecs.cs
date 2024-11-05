using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Specifications.Brand_Specs
{
    public class BrandWithRelatedProductsSpecs : BaseSpecifications<Brand>
    {
        public BrandWithRelatedProductsSpecs()
        {
            Includes.Add(B => B.Products);
        }
    }
}
