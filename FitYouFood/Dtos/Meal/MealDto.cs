using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.Meal
{
    public class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOfficial { get; set; }

        public ICollection<IngredientAmount> Ingredients { get; set; }
    }
}
