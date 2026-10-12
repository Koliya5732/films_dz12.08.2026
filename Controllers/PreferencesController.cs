
using Microsoft.AspNetCore.Mvc;

namespace films_dz12._08._2026.Controllers
{
    [ApiController]
    [Route("api/preferences")]
    public class PreferencesController : ControllerBase
    {
        [HttpPost("theme")]
        public IActionResult SetTheme([FromBody] ThemeRequest request)
        {
            if (request.Theme != "light" && request.Theme != "dark")
            {
                return BadRequest("Theme must be light or dark.");
            }

            Response.Cookies.Append("theme", request.Theme,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    HttpOnly = true,
                    Secure = Request.IsHttps,
                    SameSite = SameSiteMode.Lax
                });

            return Ok(new { message = "Theme saved", theme = request.Theme });
        }

        [HttpGet("theme")]
        public IActionResult GetTheme()
        {
            var theme = Request.Cookies["theme"] ?? "light";

            return Ok(new { theme });
        }

        [HttpPost("/api/session/username")]
        public IActionResult SetUsername([FromBody] UsernameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("Username is required.");
            }

            HttpContext.Session.SetString("Username", request.Username);

            return Ok(new { message = "Username saved" });
        }

        [HttpGet("/api/session/username")]
        public IActionResult GetUsername()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return NotFound("Username not found in session.");
            }

            return Ok(new { username });
        }
    }

    public class ThemeRequest
    {
        public string Theme { get; set; } = "light";
    }

    public class UsernameRequest
    {
        public string Username { get; set; } = "";
    }
}
