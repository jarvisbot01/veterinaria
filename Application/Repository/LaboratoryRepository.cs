using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class LaboratoryRepository : GenericRepository<Laboratory>, ILaboratory
    {
        private readonly PetShopContext _context;

        public LaboratoryRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
