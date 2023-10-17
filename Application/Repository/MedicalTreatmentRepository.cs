using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class MedicalTreatmentRepository : GenericRepository<MedicalTreatment>, IMedicalTreatment
    {
        private readonly PetShopContext _context;

        public MedicalTreatmentRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
