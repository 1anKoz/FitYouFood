using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<ICollection<Training>> GetUserTrainings(string userId);
        Task<Training> GetTraining(int trainingid);

        Task<bool> CreateTraining(Training training);
        Task<bool> UpdateTraining(Training training);

        Task<bool> SaveAsync();
        Task<bool> TrainingExistsAsync(int id);
    }
}
