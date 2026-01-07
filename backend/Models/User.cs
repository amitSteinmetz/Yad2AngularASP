using Microsoft.AspNetCore.Identity;
//using backend.Models;
using backend.Models.AssetModels;

namespace backend.Models
{
    public class User : IdentityUser
    {
        public ICollection<Asset> Assets { get; set; } = [];
    }
}
