using backend.DTOs;
using backend.IRepositories;
using Microsoft.AspNetCore.Authorization;
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
                return Ok(new { message = Resources.ClientMessages.Auth.RegistrationSuccess });
            }

            return BadRequest(result.Errors);
        }

        // login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDetails details)
        {
            var result = await _authRepository.LoginAsync(details);

            if (result == null)
                return Unauthorized(new { message = Resources.ClientMessages.Auth.InvalidCredentials });

            // יצירת העוגייה
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                MaxAge = TimeSpan.FromHours(5),
                IsEssential = true
            };

            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);

            return Ok(new { accessToken = result.AccessToken, user = result.UserDto, message = Resources.ClientMessages.Auth.LoginSuccess });
        }

        // refresh
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            // שליפת הריפרש טוקן מהעוגייה
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = Resources.ClientMessages.Auth.NoRefreshToken });
            }

            var result = await _authRepository.RefreshTokenAsync(refreshToken);

            if (result == null)
            {
                return Unauthorized(new { message = Resources.ClientMessages.Auth.RefreshTokenInvalid });
            }
          
            // עדכון העוגייה עם הריפרש טוקן החדש
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                MaxAge = TimeSpan.FromDays(7),
                IsEssential = true
            };
            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);

            return Ok(new { accessToken = result.AccessToken, user = result.UserDto });
        }

        // logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // 1. שליפת הריפרש טוקן מהעוגייה לצורך זיהוי ב-DB
            var refreshToken = Request.Cookies["refreshToken"];

            // 2. ביטול הטוקן בשרת
            await _authRepository.LogoutAsync(refreshToken);

            // 3. מחיקת העוגייה מהדפדפן
            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                IsEssential = true
            });

            return Ok(new { message = Resources.ClientMessages.Auth.LogoutSuccess });
        }
        // google login
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        {
            var result = await _authRepository.GoogleLoginAsync(googleLoginDto.IdToken);

            if (result == null)
                return BadRequest("Invalid Google Token");

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                MaxAge = TimeSpan.FromDays(7),
                IsEssential = true
            };

            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);

            return Ok(new { accessToken = result.AccessToken, user = result.UserDto, message = "Google Login Successful" });
        }
    }
}
