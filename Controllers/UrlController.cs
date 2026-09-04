using Microsoft.AspNetCore.Mvc;
using UrlShortner.Data;
using UrlShortner.Models;
using UrlShortner.Services;
namespace UrlShortner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController :ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UrlService _urlService;

    public UrlController(ApplicationDbContext context, UrlService urlService){
        _context = context;
        _urlService = urlService;
    }
    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten(string url){
        var shortUrl = new ShortUrl{
            LongUrl = url,
            code = _urlService.GenerateCode()
        };

        _context.ShortUrls.Add(shortUrl);
        await _context.SaveChangesAsync();
        return Ok(shortUrl);
    }
}