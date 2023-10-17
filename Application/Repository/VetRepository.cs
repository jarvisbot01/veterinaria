using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class VetRepository : GenericRepository<Vet>, IVet
    {
        private readonly PetShopContext _context;

        public VetRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
