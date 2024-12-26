using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
using System.Net;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRespository walkRespository;
        public WalksController(IMapper mapper, IWalkRespository walkRespository)
        {
                this.mapper = mapper;
                this.walkRespository= walkRespository;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddWalksRequestDTO addWalksRequestDTO)
        {
          var walkDomainModel=  mapper.Map<Walk>(addWalksRequestDTO);

            await walkRespository.CreateAsync(walkDomainModel);
            return Ok(mapper.Map<WalkDTO>(walkDomainModel)); 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filteron, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? IsAscending,
            [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000)
        {
            
                var walksDomainModel = await walkRespository.GetAllAsync(filteron, filterQuery, sortBy, IsAscending ?? true,
                    pageNumber, pageSize);
            //Create an exception
                 throw new Exception("this is an new exception");
                 return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var walkdomainModel = await walkRespository.GetByIdasync(id);
                if (walkdomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDTO>(walkdomainModel));
            }
            catch(Exception ex)
            {
                return Problem("");
            }
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDTO  updateWalkRequestDTO)
        {
            var walkDomainModel=mapper.Map<Walk>(updateWalkRequestDTO);
            await walkRespository.UpdateAsync(id, walkDomainModel);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRespository.DeleteAsync(id);
            if(deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));


        }


    }
}
