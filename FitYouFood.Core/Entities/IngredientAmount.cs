using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    internal class IngredientAmount
    {
        public int Amount { get; set; }

        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
