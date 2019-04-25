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
    internal class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;

        public FlightBoardViewModel(FlightBoardModel model)
        {
            this.model = model;
            this.model.PropertyChanged += (sender, args) => NotifyPropertyChanged("VM_" + args.PropertyName);
        }

        public float VM_Lon
        {
            get { return this.model.Lon; }
        }

        public float VM_Lat
        {
            get { return this.model.Lat; }
        }

        #region Commands
        #region SettingsCommand
        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => this.OnSettings()));
            }
        }
        private void OnSettings()
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }
        #endregion

        #region ConnectCommand
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => this.OnConnect()));
            }
        }
        private void OnConnect()
        {
            this.model.ConnectToServer(InfoServer.Instance);
            CommandsServer commands = CommandsServer.Instance;
            commands.Connect(ApplicationSettingsModel.Instance);
        }
        #endregion

        #region DisconnectCommand
        private ICommand _disconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                return _disconnectCommand ?? (_disconnectCommand = new CommandHandler(() => this.OnDisconnect()));
            }
        }
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
