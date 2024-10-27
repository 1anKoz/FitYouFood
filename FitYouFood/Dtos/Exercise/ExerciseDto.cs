using FitYouFood.Core.Enums;

namespace FitYouFood.API.Dtos.Exercise
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Target Target { get; set; }
        public string Description { get; set; }
        public string VisualisationUrl { get; set; }
        public int Rating { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsDeleted { get; set; }
    }
}
