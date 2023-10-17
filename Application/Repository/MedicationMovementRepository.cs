using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class MedicationMovementRepository
        : GenericRepository<MedicationMovement>,
            IMedicationMovement
    {
        private readonly PetShopContext _context;

        public MedicationMovementRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
