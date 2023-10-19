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
    public class MovementTypeController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public MovementTypeController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MovementTypeDto>>> Get()
        {
            var movementType = await _unitofwork.MovementTypes.GetAllAsync();
            return _mapper.Map<List<MovementTypeDto>>(movementType);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MovementTypeDto>>> Get11(
            [FromQuery] Params MovementTypeParams
        )
        {
            var movementType = await _unitofwork.MovementTypes.GetAllAsync(
                MovementTypeParams.PageIndex,
                MovementTypeParams.PageSize,
                MovementTypeParams.Search
            );
            var listMovementTypeDto = _mapper.Map<List<MovementTypeDto>>(movementType.records);
            return new Pager<MovementTypeDto>(
                listMovementTypeDto,
                movementType.totalRecords,
                MovementTypeParams.PageIndex,
                MovementTypeParams.PageSize,
                MovementTypeParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovementTypeDto>> Get(int id)
        {
            var movementType = await _unitofwork.MovementTypes.GetByIdAsync(id);
            if (movementType == null)
            {
                return NotFound();
            }
            return _mapper.Map<MovementTypeDto>(movementType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovementType>> Post(MovementTypeDto movementTypeDto)
        {
            var movementType = _mapper.Map<MovementType>(movementTypeDto);
            _unitofwork.MovementTypes.Add(movementType);
            await _unitofwork.SaveAsync();
            if (movementType == null)
            {
                return BadRequest();
            }
            movementTypeDto.Id = movementType.Id;
            return CreatedAtAction(nameof(Post), new { id = movementTypeDto.Id }, movementTypeDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovementTypeDto>> Put(
            int id,
            [FromBody] MovementTypeDto movementTypeDto
        )
        {
            if (movementTypeDto == null)
            {
                return NotFound();
            }
            var movementType = _mapper.Map<MovementType>(movementTypeDto);
            _unitofwork.MovementTypes.Update(movementType);
            await _unitofwork.SaveAsync();
            return movementTypeDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var movementType = await _unitofwork.MovementTypes.GetByIdAsync(id);
            if (movementType == null)
            {
                return NotFound();
            }
            _unitofwork.MovementTypes.Remove(movementType);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
