namespace UrlShortner.Models;

public class ShortUrl{
    public int Id {get; set;}
    public string LongUrl {get; set;} = string.Empty;
    public string code {get; set;} = string.Empty;
    public DateTime createdAt {get; set;} = DateTime.UtcNow;
    // public int Clicks { get; set; } = 0;
}

