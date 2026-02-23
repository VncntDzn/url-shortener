
public sealed class CreateShortLinkResponse
{
    public required string Code { get; init; }
    public required string LongUrl { get; init; }
    public required string ShortUrl { get; init; }
    public DateTimeOffset? ExpiresAt { get; init; }
}