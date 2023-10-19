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
    public class MedicalTreatmentController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public MedicalTreatmentController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicalTreatmentDto>>> Get()
        {
            var medicalTreatment = await _unitofwork.MedicalTreatments.GetAllAsync();
            return _mapper.Map<List<MedicalTreatmentDto>>(medicalTreatment);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicalTreatmentDto>>> GetPaged(
            [FromQuery] Params MedicalTreatmentParams
        )
        {
            var MedicalTreatments = await _unitofwork.MedicalTreatments.GetAllAsync(
                MedicalTreatmentParams.PageIndex,
                MedicalTreatmentParams.PageSize,
                MedicalTreatmentParams.Search
            );
            var listMedicalTreatmentDto = _mapper.Map<List<MedicalTreatmentDto>>(
                MedicalTreatments.records
            );
            return new Pager<MedicalTreatmentDto>(
                listMedicalTreatmentDto,
                MedicalTreatments.totalRecords,
                MedicalTreatmentParams.PageIndex,
                MedicalTreatmentParams.PageSize,
                MedicalTreatmentParams.Search
            );
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicalTreatmentDto>> Get(int id)
        {
            var medicalTreatment = await _unitofwork.MedicalTreatments.GetByIdAsync(id);
            if (medicalTreatment == null)
            {
                return NotFound();
            }
            return _mapper.Map<MedicalTreatmentDto>(medicalTreatment);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MedicalTreatment>> Post(
            MedicalTreatmentDto medicalTreatmentDto
        )
        {
            var medicalTreatment = _mapper.Map<MedicalTreatment>(medicalTreatmentDto);
            _unitofwork.MedicalTreatments.Add(medicalTreatment);
            await _unitofwork.SaveAsync();
            if (medicalTreatment == null)
            {
                return BadRequest();
            }
            medicalTreatmentDto.Id = medicalTreatment.Id;
            return CreatedAtAction(
                nameof(Post),
                new { id = medicalTreatmentDto.Id },
                medicalTreatmentDto
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicalTreatmentDto>> Put(
            int id,
            [FromBody] MedicalTreatmentDto medicalTreatmentDto
        )
        {
            if (medicalTreatmentDto == null)
            {
                return NotFound();
            }
            var medicalTreatment = _mapper.Map<MedicalTreatment>(medicalTreatmentDto);
            _unitofwork.MedicalTreatments.Update(medicalTreatment);
            await _unitofwork.SaveAsync();
            return medicalTreatmentDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalTreatment = await _unitofwork.MedicalTreatments.GetByIdAsync(id);
            if (medicalTreatment == null)
            {
                return NotFound();
            }
            _unitofwork.MedicalTreatments.Remove(medicalTreatment);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
