using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; } //not going to store this prop it in db. it is for dbcontext

        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
