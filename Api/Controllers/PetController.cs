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
    public class PetController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public PetController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PetDto>>> Get()
        {
            var pet = await _unitofwork.Pets.GetAllAsync();
            return _mapper.Map<List<PetDto>>(pet);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PetDto>>> GetPaged([FromQuery] Params PetParams)
        {
            var Pets = await _unitofwork.Pets.GetAllAsync(
                PetParams.PageIndex,
                PetParams.PageSize,
                PetParams.Search
            );
            var listPetDto = _mapper.Map<List<PetDto>>(Pets.records);
            return new Pager<PetDto>(
                listPetDto,
                Pets.totalRecords,
                PetParams.PageIndex,
                PetParams.PageSize,
                PetParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDto>> Get(int id)
        {
            var pet = await _unitofwork.Pets.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return _mapper.Map<PetDto>(pet);
        }

        [HttpGet("consulta1B")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pet>>> Consulta1B()
        {
            var pets = await _unitofwork.Pets.Consulta1B();
            return Ok(pets);
        }

        [HttpGet("consulta3B")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> Consulta3B()
        {
            var result = await _unitofwork.Pets.Consulta3B();
            return Ok(result);
        }

        [HttpGet("Consulta6B")]
        public async Task<ActionResult<object>> Consulta6B()
        {
            var result = await _unitofwork.Pets.Consulta6B();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pet>> Post(PetDto petDto)
        {
            var pet = _mapper.Map<Pet>(petDto);
            _unitofwork.Pets.Add(pet);
            await _unitofwork.SaveAsync();
            if (pet == null)
            {
                return BadRequest();
            }
            petDto.Id = pet.Id;
            return CreatedAtAction(nameof(Post), new { id = petDto.Id }, petDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDto>> Put(int id, [FromBody] PetDto petDto)
        {
            if (petDto == null)
            {
                return NotFound();
            }
            var pet = _mapper.Map<Pet>(petDto);
            _unitofwork.Pets.Update(pet);
            await _unitofwork.SaveAsync();
            return petDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _unitofwork.Pets.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            _unitofwork.Pets.Remove(pet);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
