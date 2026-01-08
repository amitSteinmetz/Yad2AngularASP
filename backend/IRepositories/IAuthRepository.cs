using backend.DTOs;
using Microsoft.AspNetCore.Identity;

namespace backend.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterDetails registerDetails);
        Task<(string? accessToken, string? refreshToken)> LoginAsync(LoginDetails details);
        Task<(string? accessToken, string? refreshToken)> RefreshTokenAsync(string refreshToken);
    }
}
