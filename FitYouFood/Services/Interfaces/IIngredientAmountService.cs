using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IIngredientAmountService
    {
        Task<ICollection<IngredientAmount>> GetIngredientAmounts();
        Task<IngredientAmount> GetIngredientAmount(int mealId, int ingredientId);

        Task<bool> CreateIngredientAmount(IngredientAmount ingredientAmount);
        Task<bool> UpdateIngredientAmount(IngredientAmount ingredientAmount);

        Task<bool> SaveAsync();
        Task<bool> IngredientAmountExistsAsync(int mealId, int ingredientId);
    }
}
