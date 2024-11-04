using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<ICollection<Ingredient>> GetIngredients();
        Task<Ingredient> GetIngredient(int id);

        Task<bool> CreateIngredient(Ingredient ingredient);
        Task<bool> UpdateIngredient(Ingredient ingredient);

        Task<bool> SaveAsync();
        Task<bool> IngredientExistsAsync(int id);
    }
}
