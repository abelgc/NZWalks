using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalksAsync();
        Task<List<Walk>> GetAllWalksFilteredAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetWalkById(Guid id);
        Task<Walk>CreateWalkAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);

    }
}
