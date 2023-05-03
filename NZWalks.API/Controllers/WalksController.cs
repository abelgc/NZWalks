using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionfilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //map walk domain model
            var walksDomainModel = await walkRepository.GetAllWalksAsync();

            // map those walks domain models to dto
            var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("api/[controller]/Filtered")]
        // GEt: api/Walks/api/Walks/Db?filterOn=Name&filterQuery=Track
        public async Task<IActionResult> GetAllFilteredAsync(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery]string?sortBy, 
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber =1,
            [FromQuery] int pageSize = 1000)
        {
            //map walk domain model
            var walksDomainModel = await walkRepository.GetAllWalksFilteredAsync(filterOn, filterQuery, sortBy,
                isAscending ?? true, pageNumber, pageSize);//?? is coalesce


            // map those walks domain models to dto
            var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walksDomainModel = await walkRepository.GetWalkById(id);

            var walkDTO = mapper.Map<WalkDTO>(walksDomainModel);

            return Ok(walksDomainModel != null ? walkDTO : NotFound());
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDTO addWalkRequestDto)
        {
            // Map DTO to Domain Model  (Tdestinatiopn and source in brackets)
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                // use repository to create request
                walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel);

                // Map Domain model to DTO
                var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

                return Ok(walkDTO);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody]UpdateWalkRequestDTO updateWalkRequestDto)
        {
            //map dto to domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //update
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null) return NotFound();

            //return deleted walk mapping the dto
            var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
            // map to domain model and delete
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null) return null;
            //return deleted walk mapping the dto
            var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDto);
        }

    }
}
