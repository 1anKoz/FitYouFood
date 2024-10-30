using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.Training
{
    public class TrainingUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> Difficulty { get; set; }
        public ICollection<DateTime> WhenTrained { get; set; }
    }
}
