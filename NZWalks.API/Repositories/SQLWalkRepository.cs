using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<List<Walk>> GetAllWalksFilteredAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            
            //Filstering

            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //sorting
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);

                }else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            // pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
            var walkDomainModel = await dbContext.Walks.
                Include("Difficulty").
                Include("Region").
                FirstOrDefaultAsync(w => w.Id == id);

            return walkDomainModel != null ? walkDomainModel : null;

        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            //retrieve the walk and if exists
            var existingWalkDomainModel = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            //if exists update the changes and save in db and return dto which is the param itself
            existingWalkDomainModel!.Description = walk.Description;
            existingWalkDomainModel.Name = walk.Name;
            existingWalkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            existingWalkDomainModel.RegionId = walk.RegionId;
            existingWalkDomainModel.DifficultyId = walk.DifficultyId;
            existingWalkDomainModel.LengthInKm = walk.LengthInKm;

            dbContext.SaveChangesAsync();

            return existingWalkDomainModel != null? walk: null;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomainModel =  await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (walkDomainModel == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walkDomainModel);

            await dbContext.SaveChangesAsync();

            return walkDomainModel;
        }
    }
}
