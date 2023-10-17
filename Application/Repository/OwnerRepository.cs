using Persistence;
using Domain.Entities;
using Domain.Interfaces;

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
    }
}
