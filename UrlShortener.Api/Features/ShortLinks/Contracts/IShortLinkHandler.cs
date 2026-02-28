public interface IShortLinkHandler
{

    Task<CreateShortLinkResponse> HandleAsync(CreateShortLinkRequest request, CancellationToken ct);
}
