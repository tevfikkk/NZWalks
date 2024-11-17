using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // ROUTE: api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController(IWalkRepository walkRepository, IMapper mapper) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IWalkRepository _walkRepository = walkRepository;

        // CREATE Walk
        // POST: https://localhost:portnumber/api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

            await _walkRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO and then return as a response
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        // GET Walks
        // GET: https://localhost:portnumber/api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Fetch walks
            var walksDomainModel = await _walkRepository.GetAllAsync();

            // Map Domain Model to DTO and then return it as a response
            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get Walk By Id
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);
            if (walkDomainModel is null)
                return NotFound();

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        // UPDATE Walk
        // PUT: /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel is null)
                return NotFound();

            //  Map Domain Model to DTO
            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        // Delete a Walk By Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await _walkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel is null)
                return NotFound();

            // Map Domain Model to DTO
            return Ok(_mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
}
