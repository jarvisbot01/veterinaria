using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class OwnerRepository : GenericRepository<Owner>, IOwner
    {
        private readonly PetShopContext _context;

        public OwnerRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Owner>> GetAllAsync()
        {
            return await _context.Owners.ToListAsync();
        }

        public override async Task<Owner> GetByIdAsync(int id)
        {
            return await _context.Owners.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRecords, IEnumerable<Owner> records)> GetAllAsync(
            int pageIndex,
            int pageSize,
            string search
        )
        {
            var query = _context.Owners as IQueryable<Owner>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRecords, records);
        }

        public async Task<object> Consulta5B()
        {
            var query =
                from p in _context.Owners
                select new
                {
                    Nombre = p.Name,
                    Email = p.Email,
                    Telefono = p.Phone,
                    Mascotas = (
                        from m in _context.Pets
                        join r in _context.Breeds on m.BreedId equals r.Id
                        where r.Name == "Golden Retriever" && m.OwnerId == p.Id
                        select new
                        {
                            NombreMascota = m.Name,
                            FechaNacimiento = m.Birthdate,
                            Raza = r.Name
                        }
                    ).ToList()
                };

            var result = await query.ToListAsync();
            return result;
        }
    }
}
