using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Servers;
using FlightSimulator.ViewModels;
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

namespace FlightSimulator.Views.Windows
{
    /// <summary>
    /// Interaction logic for Controls.xaml
    /// </summary>
    public partial class ManualControl : UserControl
    {
        public ManualControl()
        {
            InitializeComponent();
            DataContext = new ManualControlViewModel(new ManualControlModel());
        }
    }
}
