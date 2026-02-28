using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/shortlinks")]
public class ShortLinksController : ControllerBase
{

    private readonly ILogger<ShortLinksController> _logger;
    private readonly IShortLinkHandler _shortLinkHandler;

    public ShortLinksController(ILogger<ShortLinksController> logger, IShortLinkHandler shortLinkHandler)
    {
        _logger = logger;
        _shortLinkHandler = shortLinkHandler;
    }

    [HttpGet("{shortLink:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {

        return Ok();
    }

    [HttpPut("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateProfile(Guid userId, CancellationToken ct)
    {

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateShortLinkResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateShortLinkResponse>> Create(
           [FromBody] CreateShortLinkRequest shortLinkRequest,
           CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _shortLinkHandler.HandleAsync(shortLinkRequest, ct);

        // 201 + Location header
        return Created($"/{response.Code}", response);
    }
}
