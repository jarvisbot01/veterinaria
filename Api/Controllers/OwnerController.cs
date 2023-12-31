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
    public class OwnerController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public OwnerController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
        {
            var owners = await _unitofwork.Owners.GetAllAsync();
            return _mapper.Map<List<OwnerDto>>(owners);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<OwnerDto>>> GetPaged([FromQuery] Params ownerParams)
        {
            var owners = await _unitofwork.Owners.GetAllAsync(
                ownerParams.PageIndex,
                ownerParams.PageSize,
                ownerParams.Search
            );
            var listOwnerDto = _mapper.Map<List<OwnerDto>>(owners.records);
            return new Pager<OwnerDto>(
                listOwnerDto,
                owners.totalRecords,
                ownerParams.PageIndex,
                ownerParams.PageSize,
                ownerParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerDto>> Get(int id)
        {
            var owners = await _unitofwork.Owners.GetByIdAsync(id);
            if (owners == null)
            {
                return NotFound();
            }
            return _mapper.Map<OwnerDto>(owners);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Owner>> Post(OwnerDto ownerDto)
        {
            var owner = _mapper.Map<Owner>(ownerDto);
            _unitofwork.Owners.Add(owner);
            await _unitofwork.SaveAsync();
            if (owner == null)
            {
                return BadRequest();
            }
            ownerDto.Id = owner.Id;
            return CreatedAtAction(nameof(Post), new { id = ownerDto.Id }, ownerDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerDto>> Put(int id, [FromBody] OwnerDto ownerDto)
        {
            if (ownerDto == null)
            {
                return NotFound();
            }
            var owner = _mapper.Map<Owner>(ownerDto);
            _unitofwork.Owners.Update(owner);
            await _unitofwork.SaveAsync();
            return ownerDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _unitofwork.Owners.GetByIdAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            _unitofwork.Owners.Remove(owner);
            await _unitofwork.SaveAsync();
            return NoContent();
        }

        [HttpGet("consulta5B")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> Consulta5B()
        {
            var result = await _unitofwork.Owners.Consulta5B();
            return Ok(result);
        }
    }
}
