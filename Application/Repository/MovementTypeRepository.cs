using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class MovementTypeRepository : GenericRepository<MovementType>, IMovementType
    {
        private readonly PetShopContext _context;

        public MovementTypeRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
