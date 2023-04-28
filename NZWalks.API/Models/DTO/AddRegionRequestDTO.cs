using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min 3 characters")]
        [MaxLength(3, ErrorMessage = "Max 3 characters")]
        public string Code { get; set; } = string.Empty;
        [Required]
        [MaxLength(30, ErrorMessage = "Max 30 characters")]
        public string Name { get; set; } = string.Empty;
        public string? RegionalImageUrl { get; set; } = string.Empty;
    }
}
