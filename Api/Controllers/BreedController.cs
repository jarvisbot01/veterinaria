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
    public class BreedController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public BreedController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BreedDto>>> Get()
        {
            var breed = await _unitofwork.Breeds.GetAllAsync();
            return _mapper.Map<List<BreedDto>>(breed);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<BreedDto>>> GetPaged([FromQuery] Params breedParams)
        {
            var breeds = await _unitofwork.Breeds.GetAllAsync(
                breedParams.PageIndex,
                breedParams.PageSize,
                breedParams.Search
            );
            var listBreedDto = _mapper.Map<List<BreedDto>>(breeds.records);
            return new Pager<BreedDto>(
                listBreedDto,
                breeds.totalRecords,
                breedParams.PageIndex,
                breedParams.PageSize,
                breedParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BreedDto>> Get(int id)
        {
            var breed = await _unitofwork.Breeds.GetByIdAsync(id);
            if (breed == null)
            {
                return NotFound();
            }
            return _mapper.Map<BreedDto>(breed);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Breed>> Post(BreedDto breedDto)
        {
            var breed = _mapper.Map<Breed>(breedDto);
            _unitofwork.Breeds.Add(breed);
            await _unitofwork.SaveAsync();
            if (breed == null)
            {
                return BadRequest();
            }
            breedDto.Id = breed.Id;
            return CreatedAtAction(nameof(Post), new { id = breedDto.Id }, breedDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BreedDto>> Put(int id, [FromBody] BreedDto breedDto)
        {
            if (breedDto == null)
            {
                return NotFound();
            }
            var breed = _mapper.Map<Breed>(breedDto);
            _unitofwork.Breeds.Update(breed);
            await _unitofwork.SaveAsync();
            return breedDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var breed = await _unitofwork.Breeds.GetByIdAsync(id);
            if (breed == null)
            {
                return NotFound();
            }
            _unitofwork.Breeds.Remove(breed);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
