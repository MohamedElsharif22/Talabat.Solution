using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;
using Talabat.Repository.Specifications;

namespace Talabat.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }



        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecification<T> specs)
        {

            return await ApplySpecifications(specs).ToListAsync();
        }

        public async Task<T?> GetByIdWithSpecsAsync(ISpecification<T> specs)
        {
            return await ApplySpecifications(specs).FirstOrDefaultAsync();
        }


        private IQueryable<T> ApplySpecifications(ISpecification<T> specs)
        {
            return SpecificationsEvaluator<T>.BuildQuery(_dbContext.Set<T>(), specs);
        }

    }
}
