using HospitalManagement.Data.DataModels;

namespace HospitalManagement.Data.DataManagers
{
    public interface IAppointmentDataManager
    {
        Task<List<AppointmentModel?>> GetAllAppointmentsAsync();
        Task<AppointmentModel?> GetAppointmentByIdAsync(int id);
        Task AddAppointmentAsync(AppointmentModel? appointment);
        Task UpdateAppointmentAsync(AppointmentModel? appointment);
        Task DeleteAppointmentAsync(int id);
        int GetActiveAppointmentsCount();
    }
}
