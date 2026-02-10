using backend.Models.AssetModels;

namespace backend.DTOs
{
    public class AssetDto
    {
        public AssetType Type { get; set; }
        public AssetCondition Condition { get; set; }
        public string? Description { get; set; }
        public int? Floor { get; set; }
        public int Price { get; set; }
        public int AreaInSquareMeters { get; set; }
        public int BuiltAreaInSquareMeters { get; set; }
        public int? NumberOfRooms { get; set; }
        public DateOnly? EntryDate { get; set; }
        public AssetAddress Address { get; set; } = new();
        public ContactDetails ContactDetails { get; set; } = new();
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
        public string? MainImageUrl { get; set; }
        public List<string> GalleryImageUrls { get; set; } = [];
    }
}