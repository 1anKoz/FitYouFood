namespace FitYouFood.API.Dtos.Meal
{
    public class MealUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOfficial { get; set; }
    }
}
