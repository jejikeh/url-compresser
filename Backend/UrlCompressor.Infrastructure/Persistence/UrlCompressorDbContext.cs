using Microsoft.EntityFrameworkCore;
using UrlCompressor.Domain;

namespace UrlCompressor.Infrastructure.Persistence;

public class UrlCompressorDbContext : DbContext
{
    public DbSet<CompressedUrl> CompressedUrls { get; set; }

    public UrlCompressorDbContext(DbContextOptions<UrlCompressorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompressedUrl>().HasIndex(u => u.SmallUrl).IsUnique();
    }
}