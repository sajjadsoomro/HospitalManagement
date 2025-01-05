using HospitalManagement.App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace HospitalManagement.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Retrieve the ViewModel instance using Dependency Injection
            var viewModel = (MainWindowViewModel)App.Host.Services.GetRequiredService<MainWindowViewModel>();

            // Set the DataContext for the MainWindow to the ViewModel
            DataContext = viewModel;
        }
    }
}
