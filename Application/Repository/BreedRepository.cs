using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class BreedRepository : GenericRepository<Breed>, IBreed
    {
        private readonly PetShopContext _context;

        public BreedRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
