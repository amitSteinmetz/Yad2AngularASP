
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
                FirstName = registerDetails.FirstName,
                LastName = registerDetails.LastName,
                Email = registerDetails.Email,
                UserName = registerDetails.Email
            };

            var result = await _userManager.CreateAsync(user, registerDetails.Password);

            return result;
        }

        // login
        public async Task<AuthResult> LoginAsync(LoginDetails details)
        {
            var user = await _userManager.FindByEmailAsync(details.Email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, details.Password, false);
            if (!result.Succeeded) return null;

            // יצירת הטוקנים
            var accessToken = AuthHelpers.GenerateJwtAccessToken(user, _configuration);
            var refreshToken = AuthHelpers.GenerateRefreshToken();

            // שמירה ב-Database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            var loginResult = new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserDto = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                }
            };

            return loginResult;
        }

        // refresh
        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            // 1. חיפוש המשתמש שמחזיק בטוקן הזה
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            // 2. בדיקה שהמשתמש קיים, שהטוקן תואם ושתוקפו לא פג
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow) return null;
            

            // 3. יצירת Access Token חדש וריפרש טוקן חדש (לשיפור האבטחה - Refresh Token Rotation)
            var newAccessToken = AuthHelpers.GenerateJwtAccessToken(user, _configuration);
            var newRefreshToken = AuthHelpers.GenerateRefreshToken();

            // 4. עדכון ה-DB עם הריפרש טוקן החדש
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            var refreshResult = new AuthResult
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                UserDto = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                }
            };

            return refreshResult;
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

        // google login
        public async Task<AuthResult> GoogleLoginAsync(string idToken)
        {
            Google.Apis.Auth.GoogleJsonWebSignature.Payload payload;

            try
            {
                var settings = new Google.Apis.Auth.GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _configuration["Google:ClientId"]! }
                };

                payload = await Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch (Exception ex)
            {
                // Token invalid
                Console.WriteLine(ex.Message);
                return null;
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                // create new user
                user = new User
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded) return null;
            }

            // generate tokens and result
            var accessToken = AuthHelpers.GenerateJwtAccessToken(user, _configuration);
            var refreshToken = AuthHelpers.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserDto = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }
    }
}
