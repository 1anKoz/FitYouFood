using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Core.Entities
{
    public class IngredientAmount
    {
        public int MealId { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int Amount { get; set; }
    }
}
