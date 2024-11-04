using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.IngredientAmountDtos
{
    public class IngredientAmountGetDto
    {
        public int MealId { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int Amount { get; set; }
    }
}
