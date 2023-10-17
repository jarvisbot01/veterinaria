using Persistence;
using Domain.Entities;
using Domain.Interfaces;

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
    }
}
