using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class MovementTypeRepository : GenericRepository<MovementType>, IMovementType
    {
        private readonly PetShopContext _context;

        public MovementTypeRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MovementType>> GetAllAsync()
        {
            return await _context.MovementTypes.ToListAsync();
        }

        public override async Task<MovementType> GetByIdAsync(int id)
        {
            return await _context.MovementTypes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(
            int totalRecords,
            IEnumerable<MovementType> records
        )> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.MovementTypes as IQueryable<MovementType>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Description.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRecords, records);
        }
    }
}
