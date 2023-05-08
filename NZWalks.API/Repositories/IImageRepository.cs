﻿using NZWalks.API.Models.Domain;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
