using UrlShortner.DTOs;
namespace UrlShortner.Services;

public interface IUrlService
{
    Task<ShortenUrlResponse> ShortenUrlAsync(ShortenUrlRequest request, string baseUrl);
    Task<string?> GetOriginalUrlAsync(string code);
    Task<IEnumerable<ShortenUrlResponse>> GetAllShortUrlsAsync(string baseUrl);
    
}