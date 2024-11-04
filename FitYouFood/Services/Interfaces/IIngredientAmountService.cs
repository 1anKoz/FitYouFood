using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IIngredientAmountService
    {
        Task<ICollection<IngredientAmount>> GetIngredientAmounts();
        Task<IngredientAmount> GetIngredientAmount(int ingredientId, int mealId);

        Task<bool> CreateIngredientAmount(IngredientAmount ingredientAmount);
        Task<bool> UpdateIngredientAmount(IngredientAmount ingredientAmount);

        Task<bool> SaveAsync();
        Task<bool> ExerciseDataExistsAsync(int ingredientId, int mealId);
    }
}
