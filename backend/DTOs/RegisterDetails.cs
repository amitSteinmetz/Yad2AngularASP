using System.ComponentModel.DataAnnotations;

namespace backend.DTOs
{
    public class RegisterDetails
    {
        [Required(ErrorMessage = "חובה להזין שם משתמש")]
        [MinLength(3)]
        public string Fullname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
