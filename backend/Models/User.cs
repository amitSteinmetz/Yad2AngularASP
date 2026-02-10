using Microsoft.AspNetCore.Identity;
//using backend.Models;
using backend.Models.AssetModels;

namespace backend.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Asset> Assets { get; set; } = [];
    }
}
