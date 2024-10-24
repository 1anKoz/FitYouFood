using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOfficial { get; set; }

        public ICollection<IngredientAmount> Ingredients { get; set; }

        public ICollection<MealUser> Users { get; set; }
    }
}
