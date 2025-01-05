using HospitalManagement.App.Commands;
using HospitalManagement.Data.DataManagers;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.App.Views;
using HospitalManagement.Data.DataModels;

namespace HospitalManagement.App.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IAppointmentDataManager _appointmentDataManager;
        private static int _appointmentCount;

        // Command for Add Patient button
        public ICommand AddPatientCommand { get; }


        public DashboardViewModel(IAppointmentDataManager appointmentDataManager)
        {
            _appointmentDataManager = appointmentDataManager;

            AddPatientCommand = new RelayCommand(AddPatient);

            LoadActiveAppointmentsStatus();

            Series = new List<ISeries>
            {
                new PieSeries<double>
                {
                    Values = new List<double> { _appointmentCount },
                    Name = "Active",
                    MaxRadialColumnWidth = 60
                    //Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 3 }, // mark
                },
                new PieSeries<double> { Values = new List<double> { 3 }, Name = "Scheduled", MaxRadialColumnWidth = 60},
                new PieSeries<double> { Values = new List<double> { 4 }, Name = "Completed", MaxRadialColumnWidth = 60}
            };
        }

        // the expression above is equivalent to the next series collection:
        public IEnumerable<ISeries> Series { get; set; }
        

        private void AddPatient(object param)
        {
            // Open the AddPatientWindow as a modal
            var addPatientWindow = new PatientInfoWindow(null);

            // Show the AddPatientWindow
            addPatientWindow.ShowDialog();
        }

        private void LoadActiveAppointmentsStatus()
        {
            // Logic to check the number of active appointments
            _appointmentCount = _appointmentDataManager.GetActiveAppointmentsCount();
        }


    }
}
