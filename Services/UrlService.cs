using UrlShortner.DTOs;
using UrlShortner.Data;
using UrlShortner.Models;
using Microsoft.EntityFrameworkCore;

namespace UrlShortner.Services;

public class UrlService : IUrlService {
    private readonly ApplicationDbContext _context;

    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private int CodeLength = 6;

    public UrlService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ShortenUrlResponse> ShortenUrlAsync(ShortenUrlRequest request, string baseUrl)
    {
        var code = await GenerateUniqueCodeAsync();
        var entity = new ShortUrl
        {
            LongUrl = request.LongUrl,
            code = code
        };
        _context.ShortUrls.Add(entity);
        await _context.SaveChangesAsync();

        return new ShortenUrlResponse
        {
            code = entity.code,
            LongUrl = entity.LongUrl,
            ShortUrl = $"{baseUrl}/{entity.code}",
            createdAt = entity.createdAt
        };
        
    }

    public async Task<string?> GetOriginalUrlAsync(string code)
    {
        var entity = await _context.ShortUrls.FirstOrDefaultAsync(u => u.code == code);
        if(entity == null)
        {
            return null;
        }
        return entity.LongUrl;
    }

    public async Task<IEnumerable<ShortenUrlResponse>> GetAllShortUrlsAsync(string baseUrl)
    {
        var urls = await _context.ShortUrls.ToListAsync();
        return urls.Select(u => new ShortenUrlResponse
        {
            code = u.code,
            LongUrl =u.LongUrl,
            ShortUrl = $"{baseUrl}/{u.code}",
            createdAt = u.createdAt
        });
    }
    private async Task<string> GenerateUniqueCodeAsync()
    {
        var random = new Random();

        while (true)
        {
            var codeChars = new char[CodeLength];
            for (int i = 0; i < CodeLength; i++)
            {
                codeChars[i] = Chars[random.Next(Chars.Length)];
            }

            var code = new string(codeChars);
            var exists = await _context.ShortUrls.AnyAsync(u => u.code == code);
            if (!exists)
            {
                return code;
            }
        }
    }
}


