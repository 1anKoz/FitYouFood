using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<ICollection<Exercise>> GetExercises();
        Task<Exercise> GetExercise(int id);

        Task<bool> CreateExercise(Exercise exercise);
        Task<bool> UpdateExercise(Exercise exercise);

        Task<bool> SaveAsync();
        Task<bool> ExerciseExistsAsync(int id);
    }
}
