using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitYouFood.API.Services
{
    public class ExerciseService(FitYouFoodDbContext _context) : IExerciseService
    {
        public async Task<ICollection<Exercise>> Exercises()
        {
            return await _context.Exercises.OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<Exercise> Exercise(int id)
        {
            return await _context.Exercises.Where(e => e.Id == id).FirstOrDefaultAsync();
        }


        public async Task<bool> CreateExercise(Exercise exercise)
        {
            await _context.AddAsync(exercise);
            return await SaveAsync();
        }

        public async Task<bool> UpdateExercise(Exercise exercise)
        {
            _context.Update(exercise);
            return await SaveAsync();
        }

        //TODO: add isDeleted field in model and add delete method


        public async Task<bool> ExerciseExistsAsync(int id)
        {
            return await _context.Exercises.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
