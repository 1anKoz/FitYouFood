using FitYouFood.API.Services.Interfaces;
using FitYouFood.Core.Entities;
using FitYouFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FitYouFood.API.Services
{
    public class TrainingService(FitYouFoodDbContext _context) : ITrainingService
    {
        public async Task<Training> GetTraining(int? trainingId, string userId)
        {
            return await _context.Trainings.Where(t => t.Id == trainingId && t.UserId == userId).Include(t => t.ExerciseDatas).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Training>> GetTrainings(string userId)
        {
            return await _context.Trainings.Where(t => t.UserId == userId && !t.IsDeleted).OrderBy(t => t.Id).ToListAsync();
        }


        public async Task<bool> CreateTraining(Training training)
        {
            await _context.AddAsync(training);
            return await SaveAsync();
        }

        public async Task<bool> UpdateTraining(Training training)
        {
            _context.Update(training);
            return await SaveAsync();
        }


        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> TrainingExistsAsync(int id)
        {
            return await _context.Trainings.Where(t => !t.IsDeleted).AnyAsync(e => e.Id == id);
        }
    }
}
