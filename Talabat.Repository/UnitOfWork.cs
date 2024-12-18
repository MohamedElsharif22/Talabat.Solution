using System.Collections;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Repositories._Data;
using Talabat.Repositories.Generic_Repository;

namespace Talabat.Repositories
{
    public class UnitOfWork(StoreContext dbContext) : IUnitOfWork
    {
        private readonly StoreContext _dbContext = dbContext;
        private readonly Hashtable _repositories = [];

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.Contains(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(key, repository);
                return repository;
            }

            return _repositories[key] as GenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
            => _dbContext.DisposeAsync();


    }
}
