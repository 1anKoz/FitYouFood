using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    internal class ExerciseData
    {
        public int Id { get; set; }
        public int? Load {  get; set; }
        public int? Reps {  get; set; }
        public int? Series { get; set; }
        public DateTime WorkoutTime { get; set; }
        public DateTime WhenExercised { get; set; }

        public virtual Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
