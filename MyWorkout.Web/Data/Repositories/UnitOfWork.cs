using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Repositories
{
    using System.Linq.Expressions;
    using Entity;

    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<Exercise> ExerciseRepository { get; }
        IGenericRepository<Repeat> RepeatRepository { get; }
        IGenericRepository<WorkoutDay> WorkoutDayRepository { get; }
        IGenericRepository<Plan> PlanRepository { get; }
        Task<int> Save();
        void Rollback();

        void Load<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
            where TProperty : class
            where TEntity : class;
        void Load<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> expression)
            where TProperty : class
            where TEntity : class;
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IGenericRepository<Exercise>> _exerciseRepository;
        private readonly Lazy<IGenericRepository<Repeat>> _repeatRepository;
        private readonly Lazy<IGenericRepository<WorkoutDay>> _workoutDayRepository;
        private readonly Lazy<IGenericRepository<Plan>> _planRepository;
       

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            //init Repositories
            _exerciseRepository = new Lazy<IGenericRepository<Exercise>>(
                () => new GenericRepository<Exercise>(_context.Exercises));

            _repeatRepository = new Lazy<IGenericRepository<Repeat>>(
                () => new GenericRepository<Repeat>(_context.Repeats));

            _workoutDayRepository = new Lazy<IGenericRepository<WorkoutDay>>(
                () => new GenericRepository<WorkoutDay>(_context.WorkoutDays));

            _planRepository = new Lazy<IGenericRepository<Plan>>(
                () => new GenericRepository<Plan>(_context.Plans));
            
        }

        public IGenericRepository<Exercise> ExerciseRepository => _exerciseRepository.Value;
        public IGenericRepository<Repeat> RepeatRepository => _repeatRepository.Value;
        public IGenericRepository<WorkoutDay> WorkoutDayRepository => _workoutDayRepository.Value;
        public IGenericRepository<Plan> PlanRepository => _planRepository.Value;
       

        public async Task<int> Save() => await _context.SaveChangesAsync();

        public void Rollback()
        {
            _context.ChangeTracker.Entries()
                .ToList()
                .ForEach(x => x.Reload());
        }

        public void Load<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
            where TProperty : class  
            where TEntity : class
        {
            _context.Entry(entity)
                .Collection(expression)
                .Load();
        }

        //TODO Crete one method for load all dependecies
        public void Load<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> expression) 
            where TProperty : class
            where TEntity : class
        {
            _context.Entry(entity)
                .Reference(expression)
                .Load();
        }

        public ValueTask DisposeAsync() => _context.DisposeAsync();
    }
}
