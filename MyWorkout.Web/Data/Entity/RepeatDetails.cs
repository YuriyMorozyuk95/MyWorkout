namespace MyWorkout.Web.Data.Entity
{
    public class RepeatDetails
    {
        public int Id { get; set; }
        public int OverriddenCount { get; set; }
        public Repeat Repeat { get; set; }
        public ExerciseDetails ExerciseDetails { get; set; }

        public int? RepeatId { get; set; }
        public int? ExerciseDetailsId { get; set; }
    }
}