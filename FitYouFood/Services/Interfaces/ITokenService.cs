using FitYouFood.Core.Entities;

namespace FitYouFood.API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}   
