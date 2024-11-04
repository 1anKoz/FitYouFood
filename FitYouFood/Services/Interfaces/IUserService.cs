using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);

        Task<(bool Succeeded, string[] Errors)> UpdateUser(User user);

        Task<bool> UserExistsAsync(string id);

        Task<ICollection<Meal>> GetMeals(string userId);
        Task<bool> AddMeal(string userId, int mealId);
        Task<bool> RemoveMeal(string userId, int mealId);
    }
}
