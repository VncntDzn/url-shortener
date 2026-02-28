using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ShortLink> ShortLinks => Set<ShortLink>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var e = modelBuilder.Entity<ShortLink>();

        e.ToTable("short_links");
        e.HasKey(x => x.Id);


        e.Property(x => x.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()")   // pgcrypto
                .ValueGeneratedOnAdd();

        e.Property(x => x.Slug)
            .HasMaxLength(32)
            .IsRequired();

        e.Property(x => x.OriginalUrl)
            .HasMaxLength(2048)
            .IsRequired();

        e.Property(x => x.CreatedAtUtc)
            .IsRequired();

        // This is the concurrency/correctness backbone.
        e.HasIndex(x => x.Slug).IsUnique();

        // Optional but useful later:
        e.HasIndex(x => x.ExpiresAtUtc);
    }
}