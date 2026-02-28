using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Data;

public sealed class CreateShortLinkHandler : IShortLinkHandler
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;

    public CreateShortLinkHandler(AppDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task<CreateShortLinkResponse> HandleAsync(CreateShortLinkRequest request, CancellationToken ct)
    {

        var entity = new ShortLink
        {
            Slug = request.Slug,
            OriginalUrl = request.OriginalUrl,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = request.ExpiresAt?.UtcDateTime,
            IsDisabled = false
        };

        _db.ShortLinks.Add(entity);
        await _db.SaveChangesAsync(ct);

        var publicBaseUrl = _configuration["App:PublicBaseUrl"]?.TrimEnd('/')
            ?? "https://localhost:5001";

        return new CreateShortLinkResponse
        {
            Code = entity.Slug,
            LongUrl = entity.OriginalUrl,
            ShortUrl = $"{publicBaseUrl}/{entity.Slug}",
            ExpiresAt = request.ExpiresAt
        };
    }

    private async Task<string> GenerateUniqueSlugAsync(CancellationToken ct)
    {
        while (true)
        {
            var slug = Convert.ToHexString(Guid.NewGuid().ToByteArray()).ToLowerInvariant()[..8];
            var exists = await _db.ShortLinks.AnyAsync(x => x.Slug == slug, ct);
            if (!exists) return slug;
        }
    }
}
