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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        //private SettingsWindowViewModel settingsWindowVM;

        public SettingsWindow()
        {
            InitializeComponent();
            SettingsWindowViewModel vm = new SettingsWindowViewModel(new ApplicationSettingsModel());
            this.DataContext = vm;

            // Creating an action that closes the window when a button is pressed
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(() => this.Close());
            }
        }
    }
}
