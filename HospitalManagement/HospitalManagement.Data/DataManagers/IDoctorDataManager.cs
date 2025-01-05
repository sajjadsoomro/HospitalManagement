using HospitalManagement.Data.DataModels;

namespace HospitalManagement.Data.DataManagers
{
    public interface IDoctorDataManager
    {
        Task<IEnumerable<DoctorModel>> GetAllDoctorsAsync();
        Task<DoctorModel> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(DoctorModel doctor);
        Task UpdateDoctorAsync(DoctorModel doctor);
        Task DeleteDoctorAsync(int id);
    }
}