using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterDetails registerDetails);
        Task<AuthResult> LoginAsync(LoginDetails details);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string? refreshToken);
        Task<AuthResult> GoogleLoginAsync(string idToken);
    }
}
