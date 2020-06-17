using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Web.Data.Entity;

namespace MyWorkout.Web.Data
{
    using Entity;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Repeat> Repeats { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<Plan> Plans { get; set; }
    }
      
}
