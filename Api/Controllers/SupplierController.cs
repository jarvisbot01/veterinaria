using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class SupplierController : BaseApiController
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public SupplierController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> Get()
        {
            var supplier = await _unitofwork.Suppliers.GetAllAsync();
            return _mapper.Map<List<SupplierDto>>(supplier);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierDto>> Get(int id)
        {
            var supplier = await _unitofwork.Suppliers.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return _mapper.Map<SupplierDto>(supplier);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Supplier>> Post(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _unitofwork.Suppliers.Add(supplier);
            await _unitofwork.SaveAsync();
            if (supplier == null)
            {
                return BadRequest();
            }
            supplierDto.Id = supplier.Id;
            return CreatedAtAction(nameof(Post), new { id = supplierDto.Id }, supplierDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierDto>> Put(int id, [FromBody] SupplierDto supplierDto)
        {
            if (supplierDto == null)
            {
                return NotFound();
            }
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _unitofwork.Suppliers.Update(supplier);
            await _unitofwork.SaveAsync();
            return supplierDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _unitofwork.Suppliers.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _unitofwork.Suppliers.Remove(supplier);
            await _unitofwork.SaveAsync();
            return NoContent();
        }
    }
}
