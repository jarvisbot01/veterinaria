using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class MovementDetailRepository : GenericRepository<MovementDetail>, IMovementDetail
    {
        private readonly PetShopContext _context;

        public MovementDetailRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
