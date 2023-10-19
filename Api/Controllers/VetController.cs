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
    public class VetController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public VetController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VetDto>>> Get()
        {
            var vet = await _unitofwork.Vets.GetAllAsync();
            return _mapper.Map<List<VetDto>>(vet);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VetDto>>> Get11([FromQuery] Params VetParams)
        {
            var Vets = await _unitofwork.Vets.GetAllAsync(
                VetParams.PageIndex,
                VetParams.PageSize,
                VetParams.Search
            );
            var listVetDto = _mapper.Map<List<VetDto>>(Vets.records);
            return new Pager<VetDto>(
                listVetDto,
                Vets.totalRecords,
                VetParams.PageIndex,
                VetParams.PageSize,
                VetParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VetDto>> Get(int id)
        {
            var vet = await _unitofwork.Vets.GetByIdAsync(id);
            if (vet == null)
            {
                return NotFound();
            }
            return _mapper.Map<VetDto>(vet);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vet>> Post(VetDto vetDto)
        {
            var vet = _mapper.Map<Vet>(vetDto);
            _unitofwork.Vets.Add(vet);
            await _unitofwork.SaveAsync();
            if (vet == null)
            {
                return BadRequest();
            }
            vetDto.Id = vet.Id;
            return CreatedAtAction(nameof(Post), new { id = vetDto.Id }, vetDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VetDto>> Put(int id, [FromBody] VetDto vetDto)
        {
            if (vetDto == null)
            {
                return NotFound();
            }
            var vet = _mapper.Map<Vet>(vetDto);
            _unitofwork.Vets.Update(vet);
            await _unitofwork.SaveAsync();
            return vetDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var vet = await _unitofwork.Vets.GetByIdAsync(id);
            if (vet == null)
            {
                return NotFound();
            }
            _unitofwork.Vets.Remove(vet);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
