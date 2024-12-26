using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper
            ,ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //Get All
        [HttpGet]
       // [Authorize (Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
                logger.LogInformation("GetAll Regions action Method was involved");
                logger.LogWarning("this is warn");
                logger.LogError("this is log error");

                //Get data from data base -Domain Model

                var regionsDomain = await regionRepository.GetAllAsync();
                //var regionsDomain =await dbContext.regions.ToListAsync();

                //Map Domain Models to Dto
                //Map Domain Models to Dto
                logger.LogInformation($"Finished Getall Regions request withdata: {JsonSerializer.Serialize(regionsDomain)}");
                var regionDTO = mapper.Map<List<RegionDTO>>(regionsDomain);

                //Return DTO
                return Ok(regionDTO);

        }

        //Get Single
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        [Route("{id:Guid}")]
        public async Task< IActionResult> GetById([FromRoute] Guid id)
        {
            // Get region Domain Model from Database
             var regionDomain= await regionRepository.GetByIDAsync(id);
            //var regionDomain = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();

            }
            //MAp/Convert Region Domain model to RegionDto
            //return Dto back to client
            return Ok(mapper.Map<RegionDTO> (regionDomain));
        }

        //Post to Create New REgion
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task< IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDtO)
        {
            if (ModelState.IsValid)
            {
                //MAp or convert dto to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDtO);


                //Use Domain Model to Create Region

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);



                //Map Domain Model back to Dto
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //update
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task< IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);
            //check if region exists
            //var regionDomainModel =await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            regionDomainModel= await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map Domain Model back to DTo
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);
        }
        //Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task< IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel=await regionRepository.DeleteAsync(id);
         
            if(regionDomainModel == null)
            {
                return NotFound();
            }
           

            var regionDTO =mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);

        }

       

    }
}
