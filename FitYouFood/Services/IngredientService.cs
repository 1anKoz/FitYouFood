using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitYouFood.API.Services
{
    public class IngredientService(FitYouFoodDbContext _context) : IIngredientService
    {
        public async Task<Ingredient> GetIngredient(int id)
        {
            return await _context.Ingredients.Where(i => !i.IsDeleted && i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Ingredient>> GetIngredients()
        {
            return await _context.Ingredients.Where(e => !e.IsDeleted).OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<bool> CreateIngredient(Ingredient ingredient)
        {
            await _context.AddAsync(ingredient);
            return await SaveAsync();
        }

        public async Task<bool> UpdateIngredient(Ingredient ingredient)
        {
            _context.Update(ingredient);
            return await SaveAsync();
        }


        public async Task<bool> IngredientExistsAsync(int id)
        {
            return await _context.Ingredients.Where(i => !i.IsDeleted).AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

    }
}
