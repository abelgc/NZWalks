using System.Drawing.Drawing2D;
using System;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        // already inside nav props
        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }

        //Navigation properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
