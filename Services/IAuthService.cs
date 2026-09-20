using UrlShortner.DTOs;
namespace UrlShortner.Services;

public interface IAuthService
{
    Task<LoginSuccess> RegisterUser(RegisterUser register);
    Task<LoginSuccess> LoginUser(LoginUser login);

}