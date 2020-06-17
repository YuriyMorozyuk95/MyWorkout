using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Entity
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List <WorkoutDay> WorkoutDays { get; set; }
    }
}
