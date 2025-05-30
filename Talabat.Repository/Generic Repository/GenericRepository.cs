﻿using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Specifications;
using Talabat.Application._Data;
using Talabat.Application.Generic_Repository.Specifications;

namespace Talabat.Application.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {

            return await _dbContext.Set<T>().ToListAsync();
        }



        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecification<T> specs)
        {

            return await ApplySpecifications(specs).ToListAsync();
        }

        public async Task<T?> GetWithSpecsAsync(ISpecification<T> specs)
        {
            return await ApplySpecifications(specs).FirstOrDefaultAsync();
        }

        public Task<int> GetCountWithspecsAsync(ISpecification<T> specs)
        {
            return ApplySpecifications(specs).CountAsync();
        }


        private IQueryable<T> ApplySpecifications(ISpecification<T> specs)
        {
            return SpecificationsEvaluator<T>.BuildQuery(_dbContext.Set<T>(), specs);
        }

        public void Add(T entity) => _dbContext.Add(entity);

        public void Update(T entity) => _dbContext.Update(entity);

        public void Delete(T entity) => _dbContext.Remove(entity);
    }
}
