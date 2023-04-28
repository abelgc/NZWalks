using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
           return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
           // check if region exists
           var existsRegionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

           if (existsRegionDomainModel == null)
           {
               return null;
           }
           
           existsRegionDomainModel.Code = region.Code.ToUpper();
           existsRegionDomainModel.Name = region.Name;
           existsRegionDomainModel.RegionalImageUrl = region.RegionalImageUrl;

           dbContext.SaveChangesAsync();

           return region;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existsRegionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existsRegionDomainModel == null)
            {
                return null;
            } 

            dbContext.Regions.Remove(existsRegionDomainModel);
            await dbContext.SaveChangesAsync();

            return existsRegionDomainModel;
        }
    }
}
