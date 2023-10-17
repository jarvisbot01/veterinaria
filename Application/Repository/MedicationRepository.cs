using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class MedicationRepository : GenericRepository<Medication>, IMedication
    {
        private readonly PetShopContext _context;

        public MedicationRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
