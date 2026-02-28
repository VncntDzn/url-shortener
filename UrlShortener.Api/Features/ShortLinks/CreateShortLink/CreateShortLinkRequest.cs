using System.ComponentModel.DataAnnotations;

public sealed class CreateShortLinkRequest
{
    [Required]
    [MaxLength(2048)]
    public required string OriginalUrl { get; init; }

    [MaxLength(32)]
    public string? Slug { get; init; }

    public DateTimeOffset? ExpiresAt { get; init; }
}