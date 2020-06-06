using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Repositories
{
    using Entity;

    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Exercise> Exercises { get; }
        Task Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IRepository<Exercise>> _exerciseRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //init Repositories
            _exerciseRepository = new Lazy<IRepository<Exercise>>(
                () => new Repository<Exercise>(_context.Exercises));
        }

        public IRepository<Exercise> Exercises => _exerciseRepository.Value;
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync() => _context.DisposeAsync();
    }
}
