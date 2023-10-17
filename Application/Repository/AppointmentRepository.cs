using Persistence;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointment
    {
        private readonly PetShopContext _context;

        public AppointmentRepository(PetShopContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
