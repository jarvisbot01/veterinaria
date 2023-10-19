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
    public class MovementDetailController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public MovementDetailController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MovementDetailDto>>> Get()
        {
            var movementDetail = await _unitofwork.MovementDetails.GetAllAsync();
            return _mapper.Map<List<MovementDetailDto>>(movementDetail);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MovementDetailDto>>> GetPaged(
            [FromQuery] Params MovementDetailParams
        )
        {
            var MovementDetails = await _unitofwork.MovementDetails.GetAllAsync(
                MovementDetailParams.PageIndex,
                MovementDetailParams.PageSize,
                MovementDetailParams.Search
            );
            var listMovementDetailDto = _mapper.Map<List<MovementDetailDto>>(
                MovementDetails.records
            );
            return new Pager<MovementDetailDto>(
                listMovementDetailDto,
                MovementDetails.totalRecords,
                MovementDetailParams.PageIndex,
                MovementDetailParams.PageSize,
                MovementDetailParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovementDetailDto>> Get(int id)
        {
            var movementDetail = await _unitofwork.MovementDetails.GetByIdAsync(id);
            if (movementDetail == null)
            {
                return NotFound();
            }
            return _mapper.Map<MovementDetailDto>(movementDetail);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovementDetail>> Post(MovementDetailDto movementDetailDto)
        {
            var movementDetail = _mapper.Map<MovementDetail>(movementDetailDto);
            _unitofwork.MovementDetails.Add(movementDetail);
            await _unitofwork.SaveAsync();
            if (movementDetail == null)
            {
                return BadRequest();
            }
            movementDetailDto.Id = movementDetail.Id;
            return CreatedAtAction(
                nameof(Post),
                new { id = movementDetailDto.Id },
                movementDetailDto
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovementDetailDto>> Put(
            int id,
            [FromBody] MovementDetailDto movementDetailDto
        )
        {
            if (movementDetailDto == null)
            {
                return NotFound();
            }
            var movementDetail = _mapper.Map<MovementDetail>(movementDetailDto);
            _unitofwork.MovementDetails.Update(movementDetail);
            await _unitofwork.SaveAsync();
            return movementDetailDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var movementDetail = await _unitofwork.MovementDetails.GetByIdAsync(id);
            if (movementDetail == null)
            {
                return NotFound();
            }
            _unitofwork.MovementDetails.Remove(movementDetail);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
