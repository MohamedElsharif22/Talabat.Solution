using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();

    }
}
