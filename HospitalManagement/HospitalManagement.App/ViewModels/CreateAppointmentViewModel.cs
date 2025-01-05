using HospitalManagement.Data.DataModels;
using HospitalManagement.Data.DataManagers;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using HospitalManagement.App.Commands;
using System.Threading.Tasks;

namespace HospitalManagement.App.ViewModels
{
    public class CreateAppointmentViewModel : BaseViewModel
    {
        private readonly IDoctorDataManager _doctorDataManager;
        private readonly IPatientDataManager _patientDataManager;
        private readonly IAppointmentDataManager _appointmentDataManager;

        private DateOnly _appointmentDate = DateOnly.FromDateTime(DateTime.Now);
        private AppointmentStatus _status;
        private DoctorModel _selectedDoctor;
        private PatientModel _selectedPatient;
        private string _reasonToVisit;
        private TimeOnly _appointmentTime;

        public ObservableCollection<DoctorModel> Doctors { get; set; }
        public ObservableCollection<PatientModel> Patients { get; set; }

        public ICommand SaveAppointmentCommand { get; }

        // Define an action to close the window
        public Action CloseWindow { get; set; }

        public DateOnly AppointmentDate
        {
            get => _appointmentDate;
            set
            {
                _appointmentDate = value;
                OnPropertyChanged(nameof(AppointmentDate));
            }
        }

        public TimeOnly AppointmentTime
        {
            get => _appointmentTime;
            set
            {
                _appointmentTime = value;
                OnPropertyChanged(nameof(AppointmentTime));
            }
        }

        public AppointmentStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public PatientModel SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public DoctorModel SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }

        public string ReasonToVisit
        {
            get => _reasonToVisit;
            set
            {
                _reasonToVisit = value;
                OnPropertyChanged(nameof(ReasonToVisit));
            }
        }

        private int SelectedDoctorId => SelectedDoctor.Id;


        private int SelectedPatientId => SelectedPatient.Id;

        public CreateAppointmentViewModel(
            IDoctorDataManager doctorDataManager,
            IPatientDataManager patientDataManager,
            IAppointmentDataManager appointmentDataManager)
        {
            _doctorDataManager = doctorDataManager;
            _patientDataManager = patientDataManager;
            _appointmentDataManager = appointmentDataManager;

            SaveAppointmentCommand = new RelayCommand(SaveAppointment);
            }

        // Initialize method to set the OriginalPatient
        public async void Initialize(PatientModel? patient)
        {
            await LoadDoctorsAsync();
            await LoadPatientsAsync();

            if (patient == null)
            {
                return;
            }

            SelectedPatient = patient;
            SelectedDoctor = Doctors.First(d => d.Id == patient.Doctor.Id);
        }

        // Method to load doctors from the database
        private async Task LoadDoctorsAsync()
        {
            try
            {
                // Assuming _doctorDataManager has a method to get all doctors
                Doctors = new ObservableCollection<DoctorModel>(await _doctorDataManager.GetAllDoctorsAsync());
                OnPropertyChanged(nameof(Doctors));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading doctors: {ex.Message}");
                throw;
            }
        }

        private async Task LoadPatientsAsync()
        {
            try
            {
                Patients = new ObservableCollection<PatientModel>(await _patientDataManager.GetAllPatientsAsync());
                OnPropertyChanged(nameof(Patients));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading patients: {ex.Message}");
                throw;
            }

        }

        private async void SaveAppointment(object param)
        {
            if (SelectedDoctorId != 0 && SelectedPatientId != 0)
            {
                var appointment = new AppointmentModel
                {
                    AppointmentDateTime = AppointmentDate.ToDateTime(TimeOnly.Parse("00:00 PM")),
                    Status = Status,
                    DoctorId = SelectedDoctorId,
                    PatientId = SelectedPatientId,
                    ReasonForVisit = ReasonToVisit
                };

                await _appointmentDataManager.AddAppointmentAsync(appointment);

                // If the patient is saved successfully, close the window
                CloseWindow?.Invoke();  // Calls the window's close method
            }
            else
            {
                Console.WriteLine($"Error saving appointment");
            }

        }
    }
}
