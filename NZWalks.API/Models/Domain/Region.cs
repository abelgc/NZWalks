using System;

namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        //[Required]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionalImageUrl { get; set; }

    }
}
