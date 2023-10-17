using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class MedicationSupplierRepository
        : GenericRepository<MedicationSupplier>,
            IMedicationSupplier
    {
        private readonly PetShopContext _context;

        public MedicationSupplierRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
