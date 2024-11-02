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
        public IList<int> Difficulty { get; set; }
        public IList<DateTime> WhenTrained { get; set; }
        public IList<ExerciseData> ExerciseDatas { get; set; }
        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
