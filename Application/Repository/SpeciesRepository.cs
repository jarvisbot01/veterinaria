using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class SpeciesRepository : GenericRepository<Species>, ISpecies
    {
        private readonly PetShopContext _context;

        public SpeciesRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
