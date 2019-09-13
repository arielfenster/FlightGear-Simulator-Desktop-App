using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views;
using FlightSimulator.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// Contains the code behind of the controls found in the view display. Responsible for executing appropriate functions.
    /// </summary>
    internal class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"> A model object, used to receive updated values from and connect to the simulator </param>
        public FlightBoardViewModel(FlightBoardModel model)
        {
            this.model = model;
            this.model.PropertyChanged += (sender, args) => NotifyPropertyChanged("VM_" + args.PropertyName);
        }

        /// <summary>
        /// Longitude property function. Returns the value stored in the model object.
        /// </summary>
        public float VM_Lon
        {
            get { return this.model.Lon; }
        }

        /// <summary>
        /// Latitude property function. Returns the value stored in the model object.
        /// </summary>
        public float VM_Lat
        {
            get { return this.model.Lat; }
        }

        #region Commands
        #region SettingsCommand
        private ICommand _settingsCommand;

        /// <summary>
        /// Settings command property. Executes a function that displays the connection settings window.
        /// </summary>
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => this.OnSettings()));
            }
        }

        /// <summary>
        /// Displaying the connection settings window.
        /// </summary>
        private void OnSettings()
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }
        #endregion

        #region ConnectCommand
        private ICommand _connectCommand;

        /// <summary>
        /// Connect command property. Executes a function that initiates the connections to and from the flight simulator.
        /// </summary>
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => this.OnConnect()));
            }
        }

        /// <summary>
        /// Establishes connections to the simulator and from the simulator.
        /// </summary>
        private void OnConnect()
        {
            this.model.ConnectToServer(InfoServer.Instance);
            CommandsServer commands = CommandsServer.Instance;
            commands.Connect(ApplicationSettingsModel.Instance);
        }
        #endregion

        #region DisconnectCommand
        private ICommand _disconnectCommand;

        /// <summary>
        /// Disconnect command property. Executes a function that closes all communications with the simulator.
        /// </summary>
        public ICommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ?? (_disconnectCommand = new CommandHandler(() => this.OnDisconnect()));
            }
        }

        /// <summary>
        /// Closing the communications with the simulator as well as the second running thread.
        /// </summary>
        private void OnDisconnect()
        {
            try
            {
                this.model.Stop();
                InfoServer.Instance.Close();
                CommandsServer.Instance.Close();
                Console.WriteLine("Disconnected");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during closing connections: {0}", e);
            }
        }
        #endregion
        #endregion
    }
}
