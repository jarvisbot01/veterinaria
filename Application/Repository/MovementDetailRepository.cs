using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class MovementDetailRepository : GenericRepository<MovementDetail>, IMovementDetail
    {
        private readonly PetShopContext _context;

        public MovementDetailRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MovementDetail>> GetAllAsync()
        {
            return await _context.MovementDetails.ToListAsync();
        }

        public override async Task<MovementDetail> GetByIdAsync(int id)
        {
            return await _context.MovementDetails.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(
            int totalRecords,
            IEnumerable<MovementDetail> records
        )> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.MovementDetails as IQueryable<MovementDetail>;

            if (int.TryParse(search, out int searchValue))
            {
                query = query.Where(p => p.MedicationMovementId == searchValue);
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRecords, records);
        }
    }
}
