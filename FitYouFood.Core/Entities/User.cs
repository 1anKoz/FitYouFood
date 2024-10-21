using FitYouFood.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    internal class User
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Lifestyle Lifestyle { get; set; }

        public ICollection<ExerciseData> ExerciseDatas { get; set; }
        public ICollection<Training> Trainings { get; set; }
    }
}
