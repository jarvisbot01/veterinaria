using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpeciesDto>>> Get()
        {
            var species = await _unitofwork.Species.GetAllAsync();
            return _mapper.Map<List<SpeciesDto>>(species);
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
