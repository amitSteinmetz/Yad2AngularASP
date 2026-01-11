using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class LoginDetails
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
