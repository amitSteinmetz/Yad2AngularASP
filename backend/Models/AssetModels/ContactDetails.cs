using System.ComponentModel.DataAnnotations;

namespace backend.Models.AssetModels
{
    public class ContactDetails
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
