using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VetDto>>> Get()
        {
            var vet = await _unitofwork.Vets.GetAllAsync();
            return _mapper.Map<List<VetDto>>(vet);
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
