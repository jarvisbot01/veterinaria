using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplier
    {
        private readonly PetShopContext _context;

        public SupplierRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public override async Task<Supplier> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRecords, IEnumerable<Supplier> records)> GetAllAsync(
            int pageIndex,
            int pageSize,
            string search
        )
        {
            var query = _context.Suppliers as IQueryable<Supplier>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRecords, records);
        }

        public async Task<object> Consulta4B()
        {
            var query =
                from m in _context.Medications
                select new
                {
                    Name = m.Name,
                    Suppliers = (
                        from mp in _context.MedicationSuppliers
                        join me in _context.Medications on mp.MedicationId equals me.Id
                        join p in _context.Suppliers on mp.SupplierId equals p.Id
                        where m.Id == mp.MedicationId
                        select new { Name = p.Name, }
                    ).ToList()
                };

            var result = await query.ToListAsync();
            return result;
        }
    }
}
