using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.App.Commands;
using HospitalManagement.App.ViewModels;
using HospitalManagement.Data.DataManagers;
using HospitalManagement.Data.DataModels;

public class PatientInfoViewModel : BaseViewModel
{
    private readonly IDoctorDataManager _doctorDataManager;
    private readonly IPatientDataManager _patientDataManager;
    private readonly PatientsViewModel _patientsViewModel;
    private ObservableCollection<DoctorModel> _doctors;
    private PatientModel _editablePatient;
    private PatientModel? _originalPatient;

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    // Define an action to close the window
    public Action CloseWindow { get; set; }

    public ObservableCollection<DoctorModel> Doctors
    {
        get => _doctors;
        set
        {
            _doctors = value;
            OnPropertyChanged(nameof(Doctors));
        }
    }

    public PatientModel EditablePatient
    {
        get => _editablePatient;
        set
        {
            _editablePatient = value;
            OnPropertyChanged(nameof(EditablePatient));
        }
    }

    public PatientModel? OriginalPatient
    {
        get => _originalPatient;
        set
        {
            _originalPatient = value;
            OnPropertyChanged(nameof(OriginalPatient));
        }
    } // Bind to the form fields


    public PatientInfoViewModel(IDoctorDataManager doctorDataManager, 
        IPatientDataManager patientDataManager, PatientsViewModel patientsViewModel)
    {
        _doctorDataManager = doctorDataManager;
        _patientDataManager = patientDataManager;
        _patientsViewModel = patientsViewModel;

        SaveCommand = new RelayCommand(SavePatientExecute, SavePatientCanExecute);
        CancelCommand = new RelayCommand(Cancel);
    }

    

    // Initialize method to set the OriginalPatient
    public async void Initialize(PatientModel? patient)
    {
        await LoadDoctors();

        if (patient == null)
        {
            EditablePatient = new PatientModel() {DateOfBirth = DateTime.Today};
            return;
        }

        OriginalPatient = patient;
        // Create a copy of the patient to edit
        EditablePatient = new PatientModel
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            Address = patient.Address,
            MedicalHistory = patient.MedicalHistory,
            DoctorId = patient.DoctorId,
            Doctor = Doctors.FirstOrDefault(doc => doc.Id == patient.DoctorId)
        };
    }

    private bool SavePatientCanExecute(object arg)
    {
        return !(EditablePatient == null || string.IsNullOrWhiteSpace(EditablePatient.FirstName) ||
                 string.IsNullOrWhiteSpace(EditablePatient.LastName) ||
                 EditablePatient.DateOfBirth.Year is > 2020 or < 1920 || 
                 string.IsNullOrWhiteSpace(EditablePatient.PhoneNumber));
    }

    private void SavePatientExecute(object obj)
    {
        // Save logic here
        // Validate input data
        if (string.IsNullOrWhiteSpace(EditablePatient.FirstName) ||
            EditablePatient.DateOfBirth.Year > 2020 ||
            string.IsNullOrWhiteSpace(EditablePatient.PhoneNumber))
        {
            Console.WriteLine("Invalid input data!");
            return;
        }

        if(OriginalPatient is null)
            AddNewPatientInfo();
        else
            UpdatePatientInfo();


        // If the patient is saved successfully, close the window
        CloseWindow?.Invoke();  // Calls the window's close method

    }

    private void Cancel(object obj)
    {
        // If the patient is saved successfully, close the window
        CloseWindow?.Invoke();  // Calls the window's close method
    }

    // Method to load doctors from the database
    private async Task LoadDoctors()
    {
        try
        {
            Doctors = new ObservableCollection<DoctorModel>(await _doctorDataManager.GetAllDoctorsAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading doctors: {ex.Message}");
        }
    }

    private async void UpdatePatientInfo()
    {
        // Update the original patient's properties
        OriginalPatient.FirstName = EditablePatient.FirstName;
        OriginalPatient.LastName = EditablePatient.LastName;
        OriginalPatient.DateOfBirth = EditablePatient.DateOfBirth;
        OriginalPatient.Gender = EditablePatient.Gender;
        OriginalPatient.PhoneNumber = EditablePatient.PhoneNumber;
        OriginalPatient.Email = EditablePatient.Email;
        OriginalPatient.Address = EditablePatient.Address;
        OriginalPatient.MedicalHistory = EditablePatient.MedicalHistory;
        OriginalPatient.DoctorId = EditablePatient.Doctor.Id;

        OnPropertyChanged(nameof(OriginalPatient));


        // Save the patient using the DataManager
        try
        {
            await _patientDataManager.UpdatePatientAsync(OriginalPatient);
            Console.WriteLine("Patient saved successfully!");
            _patientsViewModel.Refresh();

        }
        catch (Exception ex)
        {
            // Handle any errors
            throw;
        }
    }

    private async void AddNewPatientInfo()
    {
        EditablePatient.DoctorId = EditablePatient.Doctor.Id;
        EditablePatient.Doctor = null; // Avoid tracking the Doctor entity
        await _patientDataManager.AddPatientAsync(EditablePatient);
        Console.WriteLine("Patient saved successfully!");
        _patientsViewModel.Refresh();
    }
}