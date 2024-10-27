
namespace FitYouFood.API.Dtos.ExerciseData
{
    public class ExerciseDataDto
    {
        public int Id { get; set; }
        public int? Load { get; set; }
        public int? Reps { get; set; }
        public int? Series { get; set; }
        public DateTime WorkoutTime { get; set; }
        public DateTime WhenExercised { get; set; }

        public int ExerciseId { get; set; }
        public string UserId { get; set; }
    }
}
