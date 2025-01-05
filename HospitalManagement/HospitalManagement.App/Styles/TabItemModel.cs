using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HospitalManagement.App.Styles
{
    public class TabItemModel
    {
        public string Title { get; set; }  // Tab title
        public string Icon { get; set; }   // MaterialDesign icon name
        public UserControl Content { get; set; } // UserControl for the tab content
    }
}
