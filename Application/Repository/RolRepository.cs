using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository
{
    public class RolRepository : GenericRepository<Rol>, IRol
    {
        private readonly PetShopContext _context;

        public RolRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
