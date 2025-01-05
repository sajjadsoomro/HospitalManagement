using HospitalManagement.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Data.DataManagers;
using System.Windows.Input;
using HospitalManagement.App.Commands;
using HospitalManagement.App.Views;

namespace HospitalManagement.App.ViewModels
{
    public class PatientsViewModel : BaseViewModel
    {
        private IPatientDataManager _patientDataManager;
        private ObservableCollection<PatientModel> _patients;
        // Command for Add Patient button
        public ICommand EditPatientInfoCommand { get; }

        public ICommand AddAppointmentInfoCommand { get; }

        public PatientsViewModel(IPatientDataManager patientDataManager)
        {
            _patientDataManager = patientDataManager;
            LoadPatients();

            EditPatientInfoCommand = new RelayCommand(EditPatientInfo);
            AddAppointmentInfoCommand = new RelayCommand(AddAppointmentInfo);
        }

        public ObservableCollection<PatientModel> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                OnPropertyChanged();
            }
        }

        public void Refresh()
        {
            LoadPatients();
        }

        // Method to load doctors from the database
        private async void LoadPatients()
        {
            try
            {
                Patients = new ObservableCollection<PatientModel>(await _patientDataManager.GetAllPatientsAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading doctors: {ex.Message}");
            }
        }
        private async void EditPatientInfo(object param)
        {
            if (param is int patientId)
            {
                // Example: Logic to open an edit dialog for the patient
                var patientToEdit = await _patientDataManager.GetPatientByIdAsync(patientId);
                if (patientToEdit != null)
                {
                    // Open an Edit window (e.g., passing the patient data)
                    var editWindow = new PatientInfoWindow(patientToEdit);
                    editWindow.ShowDialog();
                    return;
                }
            }

            var addPatientInfo = new PatientInfoWindow(null);
            addPatientInfo.ShowDialog();


        }

        private async void AddAppointmentInfo(object param)
        {
            if (param is int patientId)
            {
                // Example: Logic to open an edit dialog for the patient
                var patient = await _patientDataManager.GetPatientByIdAsync(patientId);
                if (patient != null)
                {
                    // Open an Edit window (e.g., passing the patient data)
                    var createAppointmentWindow = new CreateAppointmentWindow(patient);
                    createAppointmentWindow.ShowDialog();
                }
            }
        }

    }
}
