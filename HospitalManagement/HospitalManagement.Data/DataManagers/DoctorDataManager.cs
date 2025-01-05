using HospitalManagement.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data.DataManagers
{
    public class DoctorDataManager : IDoctorDataManager
    {
        private readonly HospitalDbContext _context;

        public DoctorDataManager(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<DoctorModel> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddDoctorAsync(DoctorModel doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(DoctorModel doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}