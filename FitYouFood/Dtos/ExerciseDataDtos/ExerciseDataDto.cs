
using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.ExerciseDataDtos
{
    public class ExerciseDataDto
    {
        public int Id { get; set; }
        public int? Load { get; set; }
        public int? Reps { get; set; }
        public int? Series { get; set; }
        public int? HowMuchMoreRepsAbleToDo { get; set; }
        public int? Difficulty { get; set; }
        public DateTime? WhenExercised { get; set; }

        public int TrainingId { get; set; }
        public int ExerciseId { get; set; }
    }
}
