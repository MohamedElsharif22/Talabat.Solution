using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repository.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdWithSpecsAsync(ISpecification<TEntity> specs);
        Task<IEnumerable<TEntity>> GetAllWithSpecsAsync(ISpecification<TEntity> specs);
    }
}
