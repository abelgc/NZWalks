using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min 3 characters")]
        [MaxLength(3, ErrorMessage = "Max 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Max 30 characters")]
        public string Name { get; set; }
        public string? RegionalImageUrl { get; set; }
    }
}
