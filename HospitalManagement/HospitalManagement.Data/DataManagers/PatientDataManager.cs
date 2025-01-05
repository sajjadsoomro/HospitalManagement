using HospitalManagement.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Data.DataManagers
{
    public class PatientDataManager : IPatientDataManager
    {
        private readonly HospitalDbContext _context;

        public PatientDataManager(HospitalDbContext context)
        {
            _context = context;
        }

        // Get all patients with related doctor information
        public async Task<IEnumerable<PatientModel>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.Doctor). // Include the related Doctor
                ToListAsync();
        }

        // Get a patient by ID with related doctor information
        public async Task<PatientModel?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Doctor) // Include the related Doctor
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Add a new patient
        public async Task AddPatientAsync(PatientModel patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        // Update an existing patient
        public async Task UpdatePatientAsync(PatientModel patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        // Delete a patient by ID
        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}