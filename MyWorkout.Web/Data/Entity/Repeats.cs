using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Entity
{
    public class Repeat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Count { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
