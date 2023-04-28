﻿using NZWalks.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);

    }
}
