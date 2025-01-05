using HospitalManagement.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data.DataManagers
{
    public class AppointmentDataManager : IAppointmentDataManager
    {
        private readonly HospitalDbContext _context;

        public AppointmentDataManager(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentModel?>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToListAsync();
        }

        public async Task<AppointmentModel?> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAppointmentAsync(AppointmentModel? appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(AppointmentModel? appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }

        public int GetActiveAppointmentsCount()
        {
            return _context.Appointments.Count(a => a.Status == AppointmentStatus.Active);
        }
    }
}