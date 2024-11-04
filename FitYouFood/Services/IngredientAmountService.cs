using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitYouFood.API.Services
{
    public class IngredientAmountService(FitYouFoodDbContext _context) : IIngredientAmountService
    {
        public async Task<IngredientAmount> GetIngredientAmount(int ingredientId, int mealId)
        {
            return await _context.IngredientsAmounts.Where(ia => ia.IngredientId == ingredientId && ia.MealId == mealId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<IngredientAmount>> GetIngredientAmounts()
        {
            return await _context.IngredientsAmounts.ToListAsync();
        }

        public async Task<bool> CreateIngredientAmount(IngredientAmount ingredientAmount)
        {
            await _context.AddAsync(ingredientAmount);
            return await SaveAsync();
        }

        public async Task<bool> UpdateIngredientAmount(IngredientAmount ingredientAmount)
        {
            _context.Update(ingredientAmount);
            return await SaveAsync();
        }


        public async Task<bool> ExerciseDataExistsAsync(int ingredientId, int mealId)
        {
            return await _context.IngredientsAmounts.AnyAsync(ia => ia.IngredientId == ingredientId && ia.MealId == mealId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
