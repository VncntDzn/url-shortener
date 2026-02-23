using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/shortlinks")]
public class ShortLinksController : ControllerBase
{


    private readonly ILogger<ShortLinksController> _logger;

    public ShortLinksController(ILogger<ShortLinksController> logger)
    {
        _logger = logger;
    }

    /*  [HttpGet(Name = "/")]
     public IEnumerable<WeatherForecast> Get()
     {
         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
         {
             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
             TemperatureC = Random.Shared.Next(-20, 55),
             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
         })
         .ToArray();
     } */

    [ProducesResponseType(typeof(CreateShortLinkResponse), StatusCodes.Status201Created)]
    [HttpPost]
    public ActionResult<CreateShortLinkResponse> Create([FromBody] CreateShortLinkRequest shortLinkRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // TODO: call your application/service layer here
        var code = "abc123";

        var response = new CreateShortLinkResponse
        {
            Code = code,
            LongUrl = shortLinkRequest.LongUrl,
            ShortUrl = $"{Request.Scheme}://{Request.Host}/{code}",
            ExpiresAt = shortLinkRequest.ExpiresAt
        };

        // 201 + Location header
        return Created($"/{code}", response);
    }
}
