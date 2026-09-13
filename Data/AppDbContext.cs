using Microsoft.EntityFrameworkCore;

using UrlShortner.Models;

namespace UrlShortner.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasMany(u => u.ShortUrls)
            .WithOne(s=> s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u =>u.Email)
            .IsUnique();
        
        modelBuilder.Entity<ShortUrl>()
            .HasIndex(s => s.code)
            .IsUnique();
    }
}