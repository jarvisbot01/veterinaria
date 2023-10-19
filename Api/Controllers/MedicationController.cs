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
    public class MedicationController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public MedicationController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicationDto>>> Get()
        {
            var medication = await _unitofwork.Medications.GetAllAsync();
            return _mapper.Map<List<MedicationDto>>(medication);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicationDto>>> Get11(
            [FromQuery] Params MedicationParams
        )
        {
            var Medications = await _unitofwork.Medications.GetAllAsync(
                MedicationParams.PageIndex,
                MedicationParams.PageSize,
                MedicationParams.Search
            );
            var listMedicationDto = _mapper.Map<List<MedicationDto>>(Medications.records);
            return new Pager<MedicationDto>(
                listMedicationDto,
                Medications.totalRecords,
                MedicationParams.PageIndex,
                MedicationParams.PageSize,
                MedicationParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicationDto>> Get(int id)
        {
            var medication = await _unitofwork.Medications.GetByIdAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            return _mapper.Map<MedicationDto>(medication);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Medication>> Post(MedicationDto medicationDto)
        {
            var medication = _mapper.Map<Medication>(medicationDto);
            _unitofwork.Medications.Add(medication);
            await _unitofwork.SaveAsync();
            if (medication == null)
            {
                return BadRequest();
            }
            medicationDto.Id = medication.Id;
            return CreatedAtAction(nameof(Post), new { id = medicationDto.Id }, medicationDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicationDto>> Put(
            int id,
            [FromBody] MedicationDto medicationDto
        )
        {
            if (medicationDto == null)
            {
                return NotFound();
            }
            var medication = _mapper.Map<Medication>(medicationDto);
            _unitofwork.Medications.Update(medication);
            await _unitofwork.SaveAsync();
            return medicationDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var medication = await _unitofwork.Medications.GetByIdAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            _unitofwork.Medications.Remove(medication);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
