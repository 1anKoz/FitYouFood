using FitYouFood.API.Dtos.IngredientAmountDtos;
using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IMealService
    {
        Task<ICollection<Meal>> GetMeals();
        Task<Meal> GetMeal(int mealId);

        Task<bool> CreateMeal(Meal meal);
        Task<bool> UpdateMeal(Meal meal);


        Task<bool> AddIngredientAmount(IngredientAmount ingredientAmount);
        Task<bool> UpdateIngredientAmount(IngredientAmount ingredientAmount);
        Task<bool> DeleteIngredientAmount(IngredientAmount ingredientAmount);


        Task<bool> SaveAsync();
        Task<bool> MealExistsAsync(int id);
    }
}
