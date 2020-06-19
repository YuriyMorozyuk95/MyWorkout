using System.Collections.Generic;

namespace MyWorkout.Web.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IGenericRepository<T> where T : class
    {
        IAsyncEnumerable<T> ReadAll();
        Task<T> Read(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public IAsyncEnumerable<T> ReadAll()
        {
            return _dbSet.AsAsyncEnumerable();
        }
        public async Task<T> Read(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
             _dbSet.Update(entity);
        }
        public async Task Delete(int id)
        {
            T game = await _dbSet.FindAsync(id);

            if (game != null)
            {
               _dbSet.Remove(game);
            }
        }
        public Task<bool> IsExist(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }
    }
}
