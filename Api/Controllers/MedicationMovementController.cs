using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class MedicationMovementController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public MedicationMovementController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicationMovementDto>>> Get()
        {
            var medicationMovement = await _unitofwork.MedicationMovements.GetAllAsync();
            return _mapper.Map<List<MedicationMovementDto>>(medicationMovement);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicationMovementDto>> Get(int id)
        {
            var medicationMovement = await _unitofwork.MedicationMovements.GetByIdAsync(id);
            if (medicationMovement == null)
            {
                return NotFound();
            }
            return _mapper.Map<MedicationMovementDto>(medicationMovement);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MedicationMovement>> Post(
            MedicationMovementDto medicationMovementDto
        )
        {
            var medicationMovement = _mapper.Map<MedicationMovement>(medicationMovementDto);
            _unitofwork.MedicationMovements.Add(medicationMovement);
            await _unitofwork.SaveAsync();
            if (medicationMovement == null)
            {
                return BadRequest();
            }
            medicationMovementDto.Id = medicationMovement.Id;
            return CreatedAtAction(
                nameof(Post),
                new { id = medicationMovementDto.Id },
                medicationMovementDto
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicationMovementDto>> Put(
            int id,
            [FromBody] MedicationMovementDto medicationMovementDto
        )
        {
            if (medicationMovementDto == null)
            {
                return NotFound();
            }
            var medicationMovement = _mapper.Map<MedicationMovement>(medicationMovementDto);
            _unitofwork.MedicationMovements.Update(medicationMovement);
            await _unitofwork.SaveAsync();
            return medicationMovementDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var medicationMovement = await _unitofwork.MedicationMovements.GetByIdAsync(id);
            if (medicationMovement == null)
            {
                return NotFound();
            }
            _unitofwork.MedicationMovements.Remove(medicationMovement);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
