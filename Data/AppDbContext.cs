using Microsoft.EntityFrameworkCore;
using UrlShortner.Models;

namespace UrlShortner.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
}