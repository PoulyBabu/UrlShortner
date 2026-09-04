namespace UrlShortner.Services;

public class UrlService{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string GenerateCode(int length = 6){
        var random = new Random();   
        return new string(
            Enumerable.Repeat(Chars, length)
                      .Select(x => x[random.Next(x.Length)])
                      .ToArray());
    
    }
}