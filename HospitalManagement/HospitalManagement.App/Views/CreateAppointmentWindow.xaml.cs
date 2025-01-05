using HospitalManagement.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HospitalManagement.Data.DataModels;

namespace HospitalManagement.App.Views
{
    /// <summary>
    /// Interaction logic for CreateAppointmentWindow.xaml
    /// </summary>
    public partial class CreateAppointmentWindow : Window
    {
        public CreateAppointmentWindow(PatientModel selectedPatient)
        {
            InitializeComponent();
            var viewModel = (CreateAppointmentViewModel)App.Host.Services.GetRequiredService<CreateAppointmentViewModel>();

            // Pass the selectedPatient to the ViewModel
            viewModel.Initialize(selectedPatient);

            DataContext = viewModel;

            // Assign the CloseWindow action to the window's Close method
            viewModel.CloseWindow = new Action(this.Close);

        }
    }
}
