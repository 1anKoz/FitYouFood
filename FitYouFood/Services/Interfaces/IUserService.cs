using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);

        Task<(bool Succeeded, string[] Errors)> UpdateUser(User user);
        Task<bool> DeleteUser(User user);

        Task<bool> UserExistsAsync(string id);
    }
}
