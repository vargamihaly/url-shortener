using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Services;

namespace UrlShortener.Presentation.Controllers;

[ApiController]
[Route("")]
public class UrlController(UrlShortenerService service) : ControllerBase
{
    [HttpPost("api/url/shorten")]
    public ActionResult<string> Shorten([FromBody] string longUrl)
    {
        var code = service.ShortenUrl(longUrl);

        var shortened = $"{Request.Scheme}://{Request.Host}/{code}";
        return Ok(shortened);
    }

    [HttpGet("{shortCode}")]
    public IActionResult RedirectToLongUrl(string shortCode)
    {
        try
        {
            var longUrl = service.ExpandUrl(shortCode);
            return Redirect(longUrl);
        }
        catch (InvalidOperationException)
        {
            return NotFound("Short URL not found");
        }
    }
}