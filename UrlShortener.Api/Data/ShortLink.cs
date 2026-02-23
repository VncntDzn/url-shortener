namespace UrlShortener.Api.Data;

public sealed class ShortLink
{
    public long Id { get; set; }
    public string Slug { get; set; } = default!;
    public string OriginalUrl { get; set; } = default!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ExpiresAtUtc { get; set; }
    public bool IsDisabled { get; set; }
}