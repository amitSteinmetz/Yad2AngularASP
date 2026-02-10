using System.ComponentModel.DataAnnotations;

namespace backend.Models.AssetModels
{
    public class AssetAddress
    {
        [Required]
        public string City { get; set; } = string.Empty;

        public string Neighborhood { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;

        //public int? buildingNumber { get; set; }
        public int? HouseNumber { get; set; }

        public int? ZipCode { get; set; }
    }
}
