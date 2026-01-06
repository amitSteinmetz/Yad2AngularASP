using System.ComponentModel.DataAnnotations;

namespace backend.Models.Asset
{
    public class AssetPublisher
    {
        // קישור למשתמש האמיתי במערכת (חובה למשתמש רשום)
        [Required]
        public string UserId { get; set; } = string.Empty;

        // שדות "צילום מצב" - למה? 
        // כי אולי המשתמש רוצה שם אחר או טלפון אחר ספציפית למודעה הזו
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
