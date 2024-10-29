using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> Difficulty { get; set; }
        public ICollection<DateTime> WhenTrained { get; set; }
        public ICollection<ExerciseData> ExerciseDatas { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
