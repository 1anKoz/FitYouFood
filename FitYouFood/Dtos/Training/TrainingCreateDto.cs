using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.Training
{

    //TODO: try to delete all IDs from all DTOs
    public class TrainingCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
