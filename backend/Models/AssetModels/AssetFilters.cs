namespace backend.Models.AssetModels
{
    public class AssetFilters
    {
        public AssetType? Type { get; set; }
        public AssetCondition? Condition { get; set; }
        public string? City { get; set; }
        public string? Neighborhood { get; set; }
        public string? Street { get; set; }
        public int? Floor { get; set; } 
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinRooms { get; set; }
        public int? MaxRooms { get; set; }
        public int? MinAreaInSquareMeters { get; set; } 
        public int? MaxAreaInSquareMeters { get; set; }
        public int? MinBuiltAreaInSquareMeters { get; set; } 
        public int? MaxBuiltAreaInSquareMeters { get; set; } 
        public DateOnly? EntryDate { get; set; } 
        public bool WithImage { get; set; } = false;
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
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
