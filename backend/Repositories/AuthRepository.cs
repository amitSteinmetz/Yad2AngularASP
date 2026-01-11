using backend.DTOs;
using backend.Helpers;
using backend.IRepositories;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // register
        public async Task<IdentityResult> RegisterAsync(RegisterDetails registerDetails)
        {
            var user = new User
            {
                FullName = registerDetails.Fullname,
                Email = registerDetails.Email,
                UserName = registerDetails.Email
            };

            var result = await _userManager.CreateAsync(user, registerDetails.Password);

            return result;
        }

        // login
        public async Task<(string? accessToken, string? refreshToken)> LoginAsync(LoginDetails details)
        {
            var user = await _userManager.FindByEmailAsync(details.Email);
            if (user == null) return (null, null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, details.Password, false);
            if (!result.Succeeded) return (null, null);

            // יצירת הטוקנים
            var accessToken = AuthHelpers.GenerateJwtAccessToken(user, _configuration);
            var refreshToken = AuthHelpers.GenerateRefreshToken();

            // שמירה ב-Database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return (accessToken, refreshToken);
        }

        // refresh
        public async Task<(string? accessToken, string? refreshToken)> RefreshTokenAsync(string refreshToken)
        {
            // 1. חיפוש המשתמש שמחזיק בטוקן הזה
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            // 2. בדיקה שהמשתמש קיים, שהטוקן תואם ושתוקפו לא פג
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return (null, null);
            }

            // 3. יצירת Access Token חדש וריפרש טוקן חדש (לשיפור האבטחה - Refresh Token Rotation)
            var newAccessToken = AuthHelpers.GenerateJwtAccessToken(user, _configuration);
            var newRefreshToken = AuthHelpers.GenerateRefreshToken();

            // 4. עדכון ה-DB עם הריפרש טוקן החדש
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return (newAccessToken, newRefreshToken);
        }

        // logout
        public async Task LogoutAsync(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) return;

            // מציאת המשתמש שהטוקן שייך לו
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user != null)
            {
                // איפוס הטוקן ב-DB
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
