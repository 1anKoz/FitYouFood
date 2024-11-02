using FitYouFood.Core.Enums;

namespace FitYouFood.API.Dtos.Account
{
    public class UserUpdateDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public Lifestyle Lifestyle { get; set; }
        public string Password { get; set; }
    }
}
