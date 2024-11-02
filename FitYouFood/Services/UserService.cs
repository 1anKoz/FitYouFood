using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FitYouFood.API.Services
{
    public class UserService(UserManager<User> _userManager) : IUserService
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

        public async Task<bool> DeleteUser(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await _userManager.Users.AnyAsync(u => u.Id == id && !u.Email.IsNullOrEmpty());
        }

    }
}
