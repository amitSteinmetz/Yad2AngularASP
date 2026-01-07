using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.AssetModels
{
    public class Asset
    {
        [Key] //  לנוחות בלבד - מגדיר מפורשות את השדה כמפתח ראשי
        public int Id { get; set; }

        [Required]
        public string PublisherId { get; set; } = string.Empty;

        public User Publisher { get; set; } = null!;

        [Required] 
        public AssetType Type { get; set; }

        [Required]
        public AssetCondition Condition { get; set; }

        [StringLength(700)] // הגבלת אורך כדי למנוע ניצול מיותר של זיכרון ב-DB
        public string? Description { get; set; }

        [Range(-1, 100)] // ולידציה בסיסית: קומה לא יכולה להיות 500- (מרתף זה -1)
        public int? Floor { get; set; }

        [Range(0, int.MaxValue)] // מחיר לא יכול להיות שלילי
        public int Price { get; set; }

        [Range(0, int.MaxValue)]
        public int AreaInSquareMeters { get; set; }

        [Range(0, int.MaxValue)]
        public int BuiltAreaInSquareMeters { get; set; }

        public DateOnly? EntryDate { get; set; }

        [Required]
        public DateOnly PublishDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [Range(1, 20)] // מספר חדרים הגיוני
        public int? NumberOfRooms { get; set; }

        [Url] // מוודא שהטקסט שמוכנס הוא בפורמט של קישור
        public string? MainImageUrl { get; set; }

        // אתחול רשימה מונע שגיאת NullReferenceException כשמנסים לעשות Add
        public List<string> GalleryImageUrls { get; set; } = new();

        // ערכי ברירת מחדל לבוליאנים (False הוא ברירת המחדל של C#, אך ציון מפורש מוסיף בהירות)
        public bool HasParking { get; set; } = false;
        public bool HasElevator { get; set; } = false;
        public bool HasSafeRoom { get; set; } = false;
        public bool HasBalcony { get; set; } = false;
        public bool HasAirConditioning { get; set; } = false;
        public bool HasStorage { get; set; } = false;
        public bool IsRenovated { get; set; } = false;
        public bool IsAccessible { get; set; } = false;
        public bool HasBars { get; set; } = false;
        public bool IsFurnished { get; set; } = false;
        public bool IsExclusive { get; set; } = false;

        [Required] // נכס חייב כתובת
        public AssetAddress Address { get; set; } = new();

        [Required] // נכס חייב מפרסם
        public ContactDetails ContactDetails { get; set; } = new();
    }
}