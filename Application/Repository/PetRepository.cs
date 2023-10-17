using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class PetRepository : GenericRepository<Pet>, IPet
    {
        private readonly PetShopContext _context;

        public PetRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
