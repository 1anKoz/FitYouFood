namespace FitYouFood.API.Dtos.IngredientDtos
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
        public bool IsOfficial { get; set; }
    }
}
