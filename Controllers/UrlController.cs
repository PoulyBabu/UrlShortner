using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.DTOs;
using UrlShortner.Data;
using UrlShortner.Models;
using UrlShortner.Services;
namespace UrlShortner.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController :ControllerBase
{
    private readonly IUrlService _urlService;
    private readonly string baseUrl = "${Request.Scheme}://{Request.Host}";

    public UrlController(IUrlService urlService){
        _urlService = urlService;
    }
    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten([FromBody] ShortenUrlRequest request){
      
      var response = await _urlService.ShortenUrlAsync(request,baseUrl);
      return Ok(response);
   }

    [HttpGet("{code}")]
    public async Task<IActionResult> RedirectToLongUrl([FromRoute] string code)
    {
        var destinationUrl = await _urlService.GetOriginalUrlAsync(code);
        if(destinationUrl == null)
        {
            return NotFound(new { message = "Short URL not found"});
        }
       return Redirect(destinationUrl);

    }
    [HttpGet]
    public async Task<IActionResult> GetAllShortUrls()
    {
        var urls = await _urlService.GetAllShortUrlsAsync(baseUrl);
        return Ok(urls);
    }
}