using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Specifications
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inpuQuery, ISpecification<TEntity> specs)
        {
            var query = inpuQuery;

            if (specs.Criteria is not null)
                query = inpuQuery.Where(specs.Criteria);

            query = specs.Includes.Aggregate(query, (currentQuery, currentExpression) => currentQuery.Include(currentExpression));

            return query;
        }
    }
}
