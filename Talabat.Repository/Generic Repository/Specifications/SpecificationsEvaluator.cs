using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repositories.Generic_Repository.Specifications
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inpuQuery, ISpecification<TEntity> specs)
        {
            var query = inpuQuery;

            //Filtering
            if (specs.Criteria is not null)
                query = inpuQuery.Where(specs.Criteria);

            // ordering
            if (specs.OrderByAsc is not null)
                query = query.OrderBy(specs.OrderByAsc);
            else if (specs.OrderByDesc is not null)
                query = query.OrderByDescending(specs.OrderByDesc);

            // Pagination
            if (specs.IsPagenationEnabled)
                query = query.Skip(specs.Skip).Take(specs.Take);

            query = specs.Includes.Aggregate(query, (currentQuery, currentExpression) => currentQuery.Include(currentExpression));

            return query;
        }
    }
}
