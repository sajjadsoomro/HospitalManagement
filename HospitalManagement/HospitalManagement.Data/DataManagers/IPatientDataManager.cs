// In the Data Access Project (e.g., HospitalManagement.Data)

using HospitalManagement.Data.DataModels;

namespace HospitalManagement.Data.DataManagers
{
    public interface IPatientDataManager
    {
        Task<IEnumerable<PatientModel>> GetAllPatientsAsync();
        Task<PatientModel?> GetPatientByIdAsync(int id);
        Task AddPatientAsync(PatientModel patient);
        Task UpdatePatientAsync(PatientModel patient);
        Task DeletePatientAsync(int id);
    }
}