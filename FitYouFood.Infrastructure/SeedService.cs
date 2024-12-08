using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Infrastructure
{
    public class SeedService
    {
        private readonly UserManager<User> _userManager;

        public SeedService(UserManager<User> userManager, FitYouFoodDbContext _context)
        {
            _userManager = userManager;
        }

        public async Task SeedDataContextAsync()
        {
            await SeedAdminAsync();
        }

        private async Task SeedAdminAsync()
        {
            var adminEmail = "admin@admin.com";
            var adminUserName = "admin";
            var adminPassword = "Admin1234!";

            // Check if the admin user already exists
            var existingUser = await _userManager.FindByEmailAsync(adminEmail);
            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true // Optional
                };

                var result = await _userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

    }
}
