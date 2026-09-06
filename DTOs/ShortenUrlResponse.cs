namespace UrlShortner.DTOs;

public class ShortenUrlResponse
{
    public string code {get; set;} = string.Empty;
    public string LongUrl {get; set;} = string.Empty;
    public string ShortUrl {get; set;} = string.Empty;
    public DateTime createdAt {get; set;}
}