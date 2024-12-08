using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IExerciseDataService
    {
        Task<ICollection<ExerciseData>> GetUserExerciseDatas(string userId);
        Task<ExerciseData> GetExerciseData(int exerciseDataId, string userId);

        Task<bool> CreateExerciseData(ExerciseData exerciseData);
        Task<bool> UpdateExerciseData(ExerciseData exerciseData);

        Task<bool> SaveAsync();
        Task<bool> ExerciseDataExistsAsync(int id);
    }
}
