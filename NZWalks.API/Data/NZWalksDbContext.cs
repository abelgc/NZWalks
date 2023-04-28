using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //DB Seed Date for Difficulties
            //easy, medium and hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("140e9582-b3e8-4e62-a465-62cc93c7a693"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("c5882d53-51ce-4012-a412-0f2b2ad58813"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("57de06a4-c559-4c17-84d7-cb09ffcc005e"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Reus",
                    Code = "SUR",
                    RegionalImageUrl = "http://www.catalonia-valencia.com/wp-content/uploads/2014/08/Reus-Travel-Guide.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Tarragona",
                    Code = "SUR",
                    RegionalImageUrl = "https://cdn.britannica.com/14/144714-050-D1641493/Roman-amphitheatre-Tarragona-Spain.jpg?w=300"
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Granollers",
                    Code = "Centre",
                    RegionalImageUrl = "https://www.thetravelpocketguide.com/wp-content/uploads/FEATURE_Places_Catalonia-759x500.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "La Garriga",
                    Code = "Interior",
                    RegionalImageUrl = "https://www.tripadvisor.com/Hotel_Feature-g488306-d482317-zft1-Hotel_Termes_La_Garriga.html"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Torredembarra",
                    Code = "SUR",
                    RegionalImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Terrassa",
                    Code = "East",
                    RegionalImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}