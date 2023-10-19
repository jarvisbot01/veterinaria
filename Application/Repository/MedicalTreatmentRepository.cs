using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class MedicalTreatmentRepository : GenericRepository<MedicalTreatment>, IMedicalTreatment
    {
        private readonly PetShopContext _context;

        public MedicalTreatmentRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MedicalTreatment>> GetAllAsync()
        {
            return await _context.MedicalTreatments.ToListAsync();
        }

        public override async Task<MedicalTreatment> GetByIdAsync(int id)
        {
            return await _context.MedicalTreatments.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(
            int totalRecords,
            IEnumerable<MedicalTreatment> records
        )> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.MedicalTreatments as IQueryable<MedicalTreatment>;

            if (int.TryParse(search, out int searchValue))
            {
                query = query.Where(p => p.AppointmentId == searchValue);
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRecords, records);
        }
    }
}
