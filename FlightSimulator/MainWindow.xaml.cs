using FlightSimulator.Views;
using FlightSimulator.ViewModels;
using FlightSimulator.Model;
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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FlightBoard board = new FlightBoard();
            FlightBoardViewModel vm = new FlightBoardViewModel(new FlightBoardModel());
            Window wnd = new Window();
            wnd.Content = board.Content;
            wnd.DataContext = vm;
            wnd.ShowDialog();
        }
    }
}
