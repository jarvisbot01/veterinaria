using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Authorize(Roles = "employee")]
    public class SpeciesController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public SpeciesController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpeciesDto>>> Get()
        {
            var species = await _unitofwork.Species.GetAllAsync();
            return _mapper.Map<List<SpeciesDto>>(species);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<SpeciesDto>>> GetPaged(
            [FromQuery] Params SpeciesParams
        )
        {
            var Species = await _unitofwork.Species.GetAllAsync(
                SpeciesParams.PageIndex,
                SpeciesParams.PageSize,
                SpeciesParams.Search
            );
            var listSpeciesDto = _mapper.Map<List<SpeciesDto>>(Species.records);
            return new Pager<SpeciesDto>(
                listSpeciesDto,
                Species.totalRecords,
                SpeciesParams.PageIndex,
                SpeciesParams.PageSize,
                SpeciesParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpeciesDto>> Get(int id)
        {
            var species = await _unitofwork.Species.GetByIdAsync(id);
            if (species == null)
            {
                return NotFound();
            }
            return _mapper.Map<SpeciesDto>(species);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Species>> Post(SpeciesDto speciesDto)
        {
            var species = _mapper.Map<Species>(speciesDto);
            _unitofwork.Species.Add(species);
            await _unitofwork.SaveAsync();
            if (species == null)
            {
                return BadRequest();
            }
            speciesDto.Id = species.Id;
            return CreatedAtAction(nameof(Post), new { id = speciesDto.Id }, speciesDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpeciesDto>> Put(int id, [FromBody] SpeciesDto speciesDto)
        {
            if (speciesDto == null)
            {
                return NotFound();
            }
            var species = _mapper.Map<Species>(speciesDto);
            _unitofwork.Species.Update(species);
            await _unitofwork.SaveAsync();
            return speciesDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var species = await _unitofwork.Species.GetByIdAsync(id);
            if (species == null)
            {
                return NotFound();
            }
            _unitofwork.Species.Remove(species);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
