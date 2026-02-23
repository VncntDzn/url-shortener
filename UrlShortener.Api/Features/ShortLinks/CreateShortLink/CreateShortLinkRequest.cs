using System.ComponentModel.DataAnnotations;

public sealed class CreateShortLinkRequest
{
    [Required]
    [MaxLength(2048)]
    public required string LongUrl { get; init; }

    [MaxLength(32)]
    public string? Alias { get; init; }

    public DateTimeOffset? ExpiresAt { get; init; }
}