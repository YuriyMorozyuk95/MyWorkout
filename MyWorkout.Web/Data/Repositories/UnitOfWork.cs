using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Repositories
{
    using Entity;

    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<Exercise> ExerciseRepository { get; }
        Task<int> Save();
        void Rollback();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IGenericRepository<Exercise>> _exerciseRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //init Repositories
            _exerciseRepository = new Lazy<IGenericRepository<Exercise>>(
                () => new GenericRepository<Exercise>(_context.Exercises));
        }

        public IGenericRepository<Exercise> ExerciseRepository => _exerciseRepository.Value;

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Rollback()
        {
            _context.ChangeTracker.Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }

        public ValueTask DisposeAsync() => _context.DisposeAsync();
    }
}
