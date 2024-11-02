using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.Training
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public DateTime WhenTrained { get; set; }
        public ICollection<ExerciseData> ExerciseDatas { get; set; }

        public string UserId { get; set; }
    }
}
