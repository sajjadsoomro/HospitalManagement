using HospitalManagement.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Data.DataManagers;

namespace HospitalManagement.App.ViewModels
{
    public class DoctorsViewModel :BaseViewModel
    {
        private IDoctorDataManager _doctorDataManager;
        private ObservableCollection<DoctorModel> _doctors;

        public DoctorsViewModel(IDoctorDataManager doctorDataManager)
        {
            _doctorDataManager = doctorDataManager;
            LoadDoctors();
        }

        public ObservableCollection<DoctorModel> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged();
            }
        }


        // Method to load doctors from the database
        private async void LoadDoctors()
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

    }
}
