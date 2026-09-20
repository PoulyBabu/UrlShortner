using UrlShortner.DTOs;

namespace UrlShortner.Services;
 public interface IJwtService
{
    string GenerateToken(JwtUser user);
}