namespace FitYouFood.API.Dtos.ExerciseData
{
    public class ExerciseDataUpdateDto
    {
        public int Id { get; set; }
        public int? Load { get; set; }
        public int? Reps { get; set; }
        public int? Series { get; set; }
        public DateTime WorkoutTime { get; set; }
        public DateTime WhenExercised { get; set; }
    }
}
