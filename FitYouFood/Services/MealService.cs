using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitYouFood.API.Services
{
    public class MealService(FitYouFoodDbContext _context) : IMealService
    {
        public async Task<Meal> GetMeal(int mealId)
        {
            return await _context.Meals.Where(m => m.Id == mealId)
                .Include(m=> m.Ingredients)
                .ThenInclude(ia => ia.Ingredient)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Meal>> GetMeals()
        {
            return await _context.Meals.OrderBy(m => m.Id)
                .Include(m => m.Ingredients)
                .ThenInclude(ia => ia.Ingredient)
                .ToListAsync();
        }
        

        public async Task<bool> CreateMeal(Meal meal)
        {
            await _context.AddAsync(meal);
            return await SaveAsync();
        }
        
        public async Task<bool> UpdateMeal(Meal meal)
        {
            _context.Meals.Update(meal);
            return await SaveAsync();
        }


        public async Task<bool> MealExistsAsync(int id)
        {
            return await _context.Meals.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> AddIngredientAmount(IngredientAmount ingredientAmount)
        {
            await _context.AddAsync(ingredientAmount);
            return await SaveAsync();
        }

        public Task<bool> UpdateIngredientAmount(IngredientAmount ingredientAmount)
        {
            _context.Update(ingredientAmount);
            return SaveAsync();
        }

        public Task<bool> DeleteIngredientAmount(IngredientAmount ingredientAmount)
        {
            _context.Remove(ingredientAmount);
            return SaveAsync();
        }
    }
}
