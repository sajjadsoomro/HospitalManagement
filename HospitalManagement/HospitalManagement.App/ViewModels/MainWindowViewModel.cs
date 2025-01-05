using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HospitalManagement.App.Commands;
using HospitalManagement.App.Views;
using HospitalManagement.Data.DataManagers;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Events;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using SkiaSharp;
using HospitalManagement.Data.DataModels;
using System.Collections.ObjectModel;
using HospitalManagement.App.Styles;

namespace HospitalManagement.App.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private TabItemModel _selectedTab;


        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<TabItemModel>
            {
                new TabItemModel
                {
                    Title = "Dashboard",
                    Icon = "ViewDashboard",
                    Content = new DashboardView() // Your UserControl
                },
                new TabItemModel
                {
                    Title = "Patients",
                    Icon = "AccountInjury",
                    Content = new PatientsView() // Your UserControl
                },
                new TabItemModel
                {
                    Title = "Doctors",
                    Icon = "Doctor",
                    Content = new DoctorsView() // Your UserControl
                }
                //new TabItemModel
                //{
                //    Title = "Search",
                //    Icon = "Magnify",
                //    Content = new SearchView() // Your UserControl
                //}
            };

            SelectedTab = Tabs[0]; // Default selected tab
        }

        public ObservableCollection<TabItemModel> Tabs { get; }

        public TabItemModel SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnPropertyChanged();
            }
        }





    }

    
}