using FitYouFood.Core.Entities;

namespace FitYouFood.API.Dtos.Meal
{
    public class MealCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOfficial { get; set; }
    }
}
