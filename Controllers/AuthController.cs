using Microsoft.AspNetCore.Mvc;
using UrlShortner.DTOs;
using UrlShortner.Services;

namespace UrlShortner.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUser register)
    {
        try
        {
            var result = await _authService.RegisterUser(register);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(new {messgae = ex.Message});
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser login)
    {
        try
        {
            var result = await _authService.LoginUser(login);
            return Ok(result);
        }
        catch(Exception ex)
        {
          return Unauthorized(new {messgae = ex.Message});  
        }

        }
    }
