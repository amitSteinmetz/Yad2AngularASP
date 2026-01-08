using backend.DTOs;
using backend.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDetails registerDetails)
        {
            var result = await _authRepository.RegisterAsync(registerDetails);

            if (result.Succeeded)
            {
                return Ok("משתמש נרשם בהצלחה, מחרוזת טקסט");
            }

            return BadRequest(result.Errors);
        }

        // login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDetails details)
        {
            var (accessToken, refreshToken) = await _authRepository.LoginAsync(details);

            if (accessToken == null || refreshToken == null)
                return Unauthorized(new { message = "אימייל או סיסמה שגויים" });

            // יצירת העוגייה
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    // מונע מ-JavaScript לגשת לעוגייה (הגנה מ-XSS)
                Secure = true,      // נשלח רק ב-HTTPS
                SameSite = SameSiteMode.Strict, // הגנה מ-CSRF
                Expires = DateTime.Now.AddHours(5)
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            return Ok(new { accessToken, message = "התחברת בהצלחה!" });
        }

        // refresh
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            // שליפת הריפרש טוקן מהעוגייה
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "No refresh token provided" });
            }

            var (newAccessToken, newRefreshToken) = await _authRepository.RefreshTokenAsync(refreshToken);

            if (newAccessToken == null || newRefreshToken == null)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            // עדכון העוגייה עם הריפרש טוקן החדש
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

            return Ok(new { accessToken = newAccessToken });
        }

        // logout
    }
}
