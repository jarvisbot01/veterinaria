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
    public class LaboratoryController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public LaboratoryController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<LaboratoryDto>>> Get()
        {
            var laboratory = await _unitofwork.Laboratories.GetAllAsync();
            return _mapper.Map<List<LaboratoryDto>>(laboratory);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<LaboratoryDto>>> Get11(
            [FromQuery] Params laboratoryParams
        )
        {
            var laboratories = await _unitofwork.Laboratories.GetAllAsync(
                laboratoryParams.PageIndex,
                laboratoryParams.PageSize,
                laboratoryParams.Search
            );
            var listLaboratoryDto = _mapper.Map<List<LaboratoryDto>>(laboratories.records);
            return new Pager<LaboratoryDto>(
                listLaboratoryDto,
                laboratories.totalRecords,
                laboratoryParams.PageIndex,
                laboratoryParams.PageSize,
                laboratoryParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaboratoryDto>> Get(int id)
        {
            var laboratory = await _unitofwork.Laboratories.GetByIdAsync(id);
            if (laboratory == null)
            {
                return NotFound();
            }
            return _mapper.Map<LaboratoryDto>(laboratory);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Laboratory>> Post(LaboratoryDto laboratoryDto)
        {
            var laboratory = _mapper.Map<Laboratory>(laboratoryDto);
            _unitofwork.Laboratories.Add(laboratory);
            await _unitofwork.SaveAsync();
            if (laboratory == null)
            {
                return BadRequest();
            }
            laboratoryDto.Id = laboratory.Id;
            return CreatedAtAction(nameof(Post), new { id = laboratoryDto.Id }, laboratoryDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaboratoryDto>> Put(
            int id,
            [FromBody] LaboratoryDto laboratoryDto
        )
        {
            if (laboratoryDto == null)
            {
                return NotFound();
            }
            var laboratory = _mapper.Map<Laboratory>(laboratoryDto);
            _unitofwork.Laboratories.Update(laboratory);
            await _unitofwork.SaveAsync();
            return laboratoryDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var laboratory = await _unitofwork.Laboratories.GetByIdAsync(id);
            if (laboratory == null)
            {
                return NotFound();
            }
            _unitofwork.Laboratories.Remove(laboratory);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
