using Microsoft.EntityFrameworkCore;


namespace UrlShortner.Models;

public class User
{
    public int Id {get; set;}
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public string Role {get; set;} = "User"; // Default role is "User"
    public ICollection<ShortUrl> ShortUrls { get; set; } = new List<ShortUrl>();
}