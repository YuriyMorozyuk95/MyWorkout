namespace MyWorkout.Web.Data.Entity
{
    public class PlanDetails
    {
        public int Id { get; set; }
        public Plan Plan { get; set; }
        public int? PlanId { get; set; }
    }
}