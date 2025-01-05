using HospitalManagement.App.ViewModels;
using HospitalManagement.Data.DataModels;
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

namespace HospitalManagement.App.Views
{
    /// <summary>
    /// Interaction logic for PatientInfoWindow.xaml
    /// </summary>
    public partial class PatientInfoWindow : Window
    {
        public PatientInfoWindow(PatientModel selectedPatient)
        {
            InitializeComponent();

            // Resolve the ViewModel from the service provider
            var viewModel = (PatientInfoViewModel)App.Host.Services.GetRequiredService<PatientInfoViewModel>();

            // Pass the selectedPatient to the ViewModel
            viewModel.Initialize(selectedPatient);

            DataContext = viewModel;

            // Assign the CloseWindow action to the window's Close method
            viewModel.CloseWindow = new Action(this.Close);
        }
    }
}
