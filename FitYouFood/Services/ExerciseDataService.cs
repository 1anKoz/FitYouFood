using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitYouFood.API.Services
{
    public class ExerciseDataService(FitYouFoodDbContext _context) : IExerciseDataService
    {
        public async Task<ExerciseData> GetExerciseData(int exerciseDataId, string userId)
        {
            return await _context.ExerciseDatas.Where(ed => ed.Id == exerciseDataId && ed.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<ExerciseData>> GetUserExerciseDatas(string userId)
        {
            return await _context.ExerciseDatas.Where(ed => ed.UserId == userId && !ed.IsDeleted).OrderBy(ed => ed.Id).ToListAsync();
        }


        public async Task<bool> CreateExerciseData(ExerciseData exerciseData)
        {
            await _context.AddAsync(exerciseData);
            return await SaveAsync();
        }

        public async Task<bool> UpdateExerciseData(ExerciseData exerciseData)
        {
            _context.Update(exerciseData);
            return await SaveAsync();
        }


        public async Task<bool> ExerciseDataExistsAsync(int exerciseDataId)
        {
            return await _context.ExerciseDatas.Where(ed => !ed.IsDeleted).AnyAsync(ed => ed.Id == exerciseDataId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
