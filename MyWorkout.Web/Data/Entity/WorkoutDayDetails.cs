using System;

namespace MyWorkout.Web.Data.Entity
{
    public class WorkoutDayDetails
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public PlanDetails PlanDetails { get; set; }
        public WorkoutDay WorkoutDay { get; set; }

        public int? PlanDetailsId { get; set; }
        public int? WorkoutDayId { get; set; }
    }
}
