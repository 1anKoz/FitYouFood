using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    internal class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DateTime> WhenTrained { get; set; }
        public ICollection<ExerciseData> Exercises { get; set; }
    }
}
