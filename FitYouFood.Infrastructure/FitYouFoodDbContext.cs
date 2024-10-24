using FitYouFood.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitYouFood.Infrastructure
{
    public class FitYouFoodDbContext : IdentityDbContext<User>
    {
        public FitYouFoodDbContext(DbContextOptions<FitYouFoodDbContext> options) : base(options)
        {
            
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseData> ExerciseDatas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientAmount> IngredientsAmounts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IngredientAmount>()
                .HasKey(ia => new { ia.IngredientId, ia.MealId });

            modelBuilder.Entity<MealUser>()
                .HasKey(mu => new {mu.UserId, mu.MealId});
            modelBuilder.Entity<MealUser>()
                .HasOne(mu => mu.User)
                .WithMany(mu => mu.Meals)
                .HasForeignKey(mu => mu.UserId);
            modelBuilder.Entity<MealUser>()
                .HasOne(mu => mu.Meal)
                .WithMany(mu => mu.Users)
                .HasForeignKey(mu => mu.MealId);
        }
    }
}
