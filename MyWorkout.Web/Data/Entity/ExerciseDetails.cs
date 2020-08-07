namespace MyWorkout.Web.Data.Entity
{
    public class ExerciseDetails
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public WorkoutDayDetails WorkoutDayDetails { get; set; }

        public int? ExerciseId { get; set; }
        public int? WorkoutDayDetailsId { get; set; }
    }
}