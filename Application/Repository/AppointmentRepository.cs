using Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public override async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public override async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(
            int totalRecords,
            IEnumerable<Appointment> records
        )> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Appointments as IQueryable<Appointment>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Description.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Id);
            var totalRecords = await query.CountAsync();
            var records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRecords, records);
        }
    }
}
