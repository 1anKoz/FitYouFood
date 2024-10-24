using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    public class MealUser
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
