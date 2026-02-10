using backend.DTOs;

namespace backend.Models
{
    public class AuthResult
    {
        public UserDto UserDto { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
