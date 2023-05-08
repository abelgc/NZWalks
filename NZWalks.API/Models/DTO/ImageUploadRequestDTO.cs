using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    // public record ImageUploadRequestDTO([Required] IFormFile File, [Required] string FileName, string? FileDescription);

    public class ImageUploadRequestDTO
    {
        [Required] public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        public string? FileDescription { get; set; }
    }
}
