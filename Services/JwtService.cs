using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using UrlShortner.DTOs;

namespace UrlShortner.Services;
public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(JwtUser user)
    {
        var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
        var issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT issuer is not configured.");
        var audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT audience is not configured.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
        var token = new SecurityTokenDescriptor
        { Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"] ?? "60")),
            SigningCredentials = credentials,
            Issuer = issuer,
            Audience = audience
        
            
        };
           
        return new JsonWebTokenHandler().CreateToken(token);

    }
}