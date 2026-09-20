using UrlShortner.DTOs;
namespace UrlShortner.Services;

public interface IUrlService
{
    Task<ShortenUrlResponse> ShortenUrlAsync(ShortenUrlRequest request, string baseUrl, int UserId);
    Task<string?> GetOriginalUrlAsync(string code);
    Task<IEnumerable<ShortenUrlResponse>> GetAllShortUrlsAsync(string baseUrl,int UserId);
    
}