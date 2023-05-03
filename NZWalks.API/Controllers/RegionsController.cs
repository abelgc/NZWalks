using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //[HttpGet]
        //[Route("api/[controller]/noDb")]
        //public IActionResult GetAllRegionsNoDataBase()
        //{
        //    var regions = new List<RegionDTO>
        //    {
        //        new RegionDTO{Id=new Guid(),Name="Tarragona",Code="Sur",RegionalImageUrl = null},
        //        new RegionDTO{Id=new Guid(),Name="Reus",Code="Sur",RegionalImageUrl = null}
        //    };

        //    return Ok(regions);
        //}

        [HttpGet]
        [Route("api/[controller]/Db")]
        public async Task<IActionResult> GetAllRegions()
        {
            try
            {
                logger.LogInformation("GetAllRegions Action Method was invoked");
                // get data from db - domain models
                var regionsDomainModel = await regionRepository.GetAllRegionsAsync();

                // map those domain models to our dtos
                //var regionsDTO = new List<RegionDTO>();

                //foreach (var regionDomain in regionsDomainModel)
                //{
                //    regionsDTO.Add( new RegionDTO()
                //    {
                //        Id = regionDomain.Id,
                //        Name = regionDomain.Name,
                //        Code = regionDomain.Code,
                //        RegionalImageUrl = regionDomain.RegionalImageUrl

                //    });
                //}
                // return dtos

                //Map domain model to DTO's with AutoMapper
                var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomainModel); //Tdestinatiopn and source in brackets

                logger.LogInformation($"Returned with data: {JsonSerializer.Serialize(regionsDomainModel)} in {nameof(this.GetAllRegions)} Method");

               throw new Exception("Custom Exception");

                return Ok(regionsDTO);
            }
            catch(Exception e)
            {
                logger.LogError(e,e.Message);
                throw;
            }
           
        }

        [HttpGet]
        [Route("api/[controller]/Db/{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var regionDomainModel = await regionRepository.GetByIdAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // map region domain model to region dto
            //var regionsDTO =new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionalImageUrl = regionDomainModel.RegionalImageUrl

            //};

            //Map domain model to DTO's with AutoMapper
            var regionsDTO = mapper.Map<RegionDTO>(regionDomainModel); //Tdestinatiopn and source in brackets

            return Ok(regionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody]AddRegionRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                // map request to region domain model
                //var requestRegionDomainModel = new Region
                //{
                //    Code = request.Code.ToUpper(),
                //    Name = request.Name,
                //    RegionalImageUrl = request.RegionalImageUrl
                //};

                //Map domain model to DTO's with AutoMapper
                var requestRegionDomainModel = mapper.Map<Region>(request);

                //Use region domain model to create a region in the DB
                requestRegionDomainModel = await regionRepository.CreateAsync(requestRegionDomainModel);

                // create the regionDTO to return to the user
                //var regionDTO = new RegionDTO
                //{
                //    Code = requestRegionDomainModel.Code.ToUpper(),
                //    Name = requestRegionDomainModel.Name,
                //    RegionalImageUrl = requestRegionDomainModel.RegionalImageUrl
                //};

                var regionDTO = mapper.Map<RegionDTO>(requestRegionDomainModel); //Tdestinatiopn and source in brackets

                //return Ok(requestRegionDomainModel); POST doesnt return 200 code
                return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        [Route("api/[controller]/Db/{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody]UpdateRegionRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                //Map dto to domain model
                //var regionDomainModel = new Region()
                //{
                //    Code = request.Code.ToUpper(),
                //    Name = request.Name,
                //    RegionalImageUrl = request.RegionalImageUrl
                //};
                var regionDomainModel = mapper.Map<Region>(request);
                //Check if region exists and is not null
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // update region domain model
                //regionDomainModel.Code = request.Code.ToUpper();
                //regionDomainModel.Name = request.Name;
                //regionDomainModel.RegionalImageUrl = request.RegionalImageUrl;

                // save changes to DB
                //await dbContext.SaveChangesAsync();
                //return region dto

                //var regionDTO = new RegionDTO
                //{
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionalImageUrl = regionDomainModel.RegionalImageUrl
                //};
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel); //Tdestinatiopn and source in brackets

                return Ok(regionDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("api/[controller]/Db/{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //check if region exists
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //return deleted region back

            //var regionDTO = new RegionDTO
            //{
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionalImageUrl = regionDomainModel.RegionalImageUrl
            //};

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);

        }
    }
}
