using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.ExerciseDataDtos
{
    public class ExerciseDataUpdateDto
    {
        public int Id { get; set; }
        public int? HowMuchMoreRepsAbleToDo { get; set; }
        public int? Difficulty { get; set; }
        public int? TrainingId { get; set; }
    }
}
