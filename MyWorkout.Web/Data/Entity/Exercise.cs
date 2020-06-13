using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkout.Web.Data.Entity
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan RestTime { get; set; }

        public List<Repeat> Repeats { get; set; }
    }
}
