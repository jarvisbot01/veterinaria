using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class MedicationMovementRepository
        : GenericRepository<MedicationMovement>,
            IMedicationMovement
    {
        private readonly PetShopContext _context;

        public MedicationMovementRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MedicationMovement>> GetAllAsync()
        {
            return await _context.MedicationMovements.ToListAsync();
        }

        public override async Task<MedicationMovement> GetByIdAsync(int id)
        {
            return await _context.MedicationMovements.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(
            int totalRecords,
            IEnumerable<MedicationMovement> records
        )> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.MedicationMovements as IQueryable<MedicationMovement>;

            if (int.TryParse(search, out int searchValue))
            {
                query = query.Where(p => p.MedicationId == searchValue);
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query
                .Include(p => p.MovementType)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRecords, records);
        }
    }
}
