using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWorkout.Web.Data.Entity;

namespace MyWorkout.Web.Data
{
    using System;
    using Entity;
    using Entity.Enums;

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

        public DbSet<PlanDetails> PlanDetails { get; set; }
        public DbSet<WorkoutDayDetails> WorkoutDayDetails { get; set; }
        public DbSet<ExerciseDetails> ExerciseDetails { get; set; }
        public DbSet<RepeatDetails> RepeatDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var testPlan = new Plan
            {
                Id = 1,
                Name = "TestPlan"
            };

            var mondayWorkOutDay = new WorkoutDay()
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                MuscleGroup = MuscleGroup.Back,
                PlanId = testPlan.Id
            };
            var fridayWorkOutDay = new WorkoutDay()
            {
                Id = 2,
                DayOfWeek = DayOfWeek.Friday,
                MuscleGroup = MuscleGroup.Body,
                PlanId = testPlan.Id
            };

            var pussUpsExercise = new Exercise()
            {
                Id = 1,
                Name = "Push-ups",
                RestTime = new TimeSpan(0, 0, 30),
                WorkoutDayId = mondayWorkOutDay.Id
            };

            var repeat1 = new Repeat()
            {
                Id = 1,
                Number = 1,
                Count = 5,
                ExerciseId = pussUpsExercise.Id
            };
            var repeat2 = new Repeat()
            {
                Id = 2,
                Number = 2,
                Count = 10,
                ExerciseId = pussUpsExercise.Id
            };
            var repeat3 = new Repeat()
            {
                Id = 3,
                Number = 3,
                Count = 15,
                ExerciseId = pussUpsExercise.Id
            };

            modelBuilder.Entity<Plan>().HasData(testPlan);
            modelBuilder.Entity<WorkoutDay>().HasData(mondayWorkOutDay, fridayWorkOutDay);
            modelBuilder.Entity<Exercise>().HasData(pussUpsExercise);
            modelBuilder.Entity<Repeat>().HasData(repeat1, repeat2, repeat3);

            base.OnModelCreating(modelBuilder);
        }
    }
      
}
