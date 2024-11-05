using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Specifications.Category_Specs
{
    public class CategoryWithRelatedProductsSpecifications : BaseSpecifications<Category>
    {
        public CategoryWithRelatedProductsSpecifications()
        {
            Includes.Add(C => C.Products);
        }
        public CategoryWithRelatedProductsSpecifications(Expression<Func<Category, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            Includes.Add(C => C.Products);
        }
    }
}
