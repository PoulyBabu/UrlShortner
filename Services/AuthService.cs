using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;
using UrlShortner.DTOs;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    public AuthService(ApplicationDbContext context,IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<LoginSuccess> LoginUser(LoginUser login)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == login.Email);
        if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
        {   var token = _jwtService.GenerateToken(
            new JwtUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            }
        );
            return new LoginSuccess
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }

        throw new Exception("Invalid Email or Password");
    }

    public async Task<LoginSuccess> RegisterUser(RegisterUser register)
    { if(await _context.Users.AnyAsync(u =>u.Email == register.Email))
        {
            throw new InvalidOperationException("User with this email already exists.");
        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
        var user = new User
        {
            Username = register.Username,
            Email = register.Email,
            PasswordHash = passwordHash,
            Role = "User"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new LoginSuccess
        {
            Id  = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };

    }
}