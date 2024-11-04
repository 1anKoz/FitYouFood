using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FitYouFood.API.Services
{
    public class UserService(UserManager<User> _userManager, FitYouFoodDbContext _context) : IUserService
    {
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await _userManager.Users.AnyAsync(u => u.Id == id && !u.Email.IsNullOrEmpty());
        }


        public async Task<ICollection<Meal>> GetMeals(string userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Meals)
                .ThenInclude(mu => mu.Meal)
                .ThenInclude(m => m.Ingredients)
                .ThenInclude(ia => ia.Ingredient)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return [];

            return user.Meals.Select(mu => mu.Meal).ToList();
        }

        public async Task<bool> AddMeal(string userId, int mealId)
        {
            var user = await _userManager.Users.Include(u => u.Meals)
                                            .FirstOrDefaultAsync(u => u.Id == userId);
            var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == mealId);

            if (user == null || meal == null)
                return false;

            if (!user.Meals.Any(mu => mu.MealId == mealId))
            {
                user.Meals.Add(new MealUser { UserId = userId, MealId = mealId });
                return await SaveAsync();
            }

            return true;
        }

        public async Task<bool> RemoveMeal(string userId, int mealId)
        {
            var mealUser = await _context.MealUser
                .FirstOrDefaultAsync(mu => mu.UserId == userId && mu.MealId == mealId);

            if (mealUser == null)
                return false;

            _context.MealUser.Remove(mealUser);
            return await SaveAsync();
        }


        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
