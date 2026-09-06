using System.ComponentModel.DataAnnotations;
namespace UrlShortner.DTOs;

public class ShortenUrlRequest
{
    [Required(ErrorMessage = "URL is required")]
    [Url(ErrorMessage = "Please enter a valid HTTP OR HTTPSURL")]
    public string LongUrl {get; set;} = string.Empty;
}