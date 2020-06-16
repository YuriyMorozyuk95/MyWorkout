using MyWorkout.Web.Data.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Entity
{
    public class WorkoutDay
    {
       public int Id { get; set; }
       public DayOfWeek DayOfWeek { get; set; }
       public MuscleGroup MuscleGroup { get; set; }
       public List <Exercise> Exercises { get; set; }
       public Plan Plan { get; set; }
       public int PlanId { get; set; }
       

       

    }
}
