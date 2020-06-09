using System.Collections.Generic;

namespace MyWorkout.Web.Data.Repositories
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> EntitySet { get; }
        IAsyncEnumerable<T> ReadAll();
        Task<T> Read(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }
        //TODO remove after DAL will be done
        public DbSet<T> EntitySet => _dbSet;

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
    }
}
