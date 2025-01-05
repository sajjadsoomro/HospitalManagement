using System;
using System.Configuration;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HospitalManagement.Data;
using HospitalManagement.Data.DataManagers;
using HospitalManagement.App.ViewModels;
using HospitalManagement.App.Views;
using Microsoft.Extensions.Hosting;
using LiveChartsCore;

namespace HospitalManagement.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost Host;

        // The application's entry point
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and build the host
            Host = CreateHostBuilder(e.Args).Build();

            // Get and show the main window with dependencies injected
            var mainWindow = Host.Services.GetService(typeof(MainWindow)) as MainWindow;
            mainWindow.Show();
        }

        // Override OnExit to stop the host when the application exits
        protected override void OnExit(ExitEventArgs e)
        {
            Host?.Dispose();
            base.OnExit(e);
        }

        // Configure the host and services
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["HospitalDBContext"].ConnectionString;
                    // Add the DbContext and configure it with a connection string
                    services.AddDbContext<HospitalDbContext>(options =>
                        options.UseSqlServer(connectionString), ServiceLifetime.Transient);

                    // Register the DataManagers (services) to the DI container
                    services.AddScoped<IPatientDataManager, PatientDataManager>();
                    services.AddScoped<IDoctorDataManager, DoctorDataManager>();
                    services.AddScoped<IAppointmentDataManager, AppointmentDataManager>();

                    RegisterViews(services);

                    RegisterViewModels(services);

                });


        private static void RegisterViews(IServiceCollection services)
        {
            // Register Windows
            services.AddSingleton<MainWindow>();
            services.AddSingleton<CreateAppointmentWindow>();
            services.AddSingleton<DashboardView>();
            services.AddSingleton<PatientsView>();
            services.AddSingleton<DoctorsView>();
            services.AddSingleton<PatientInfoWindow>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            // Register ViewModel
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<CreateAppointmentViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<PatientsViewModel>();
            services.AddSingleton<DoctorsViewModel>();
            services.AddSingleton<PatientInfoViewModel>();
        }
    }
}
