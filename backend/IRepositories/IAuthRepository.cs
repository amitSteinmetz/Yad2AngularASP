using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterDetails registerDetails);
        Task<LoginResult> LoginAsync(LoginDetails details);
        Task<(string? accessToken, string? refreshToken)> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string? refreshToken);
    }
}
