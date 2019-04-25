using FlightSimulator.Model;
using FlightSimulator.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    class AutoControlViewModel : BaseNotify
    {
        private string line;
        private ICommand m_OkCommand;
        private ICommand m_ClearCommand;
        private readonly AutoControlModel model;
        private bool okClicked = false;

        public AutoControlViewModel(AutoControlModel model)
        {
            this.line = "";
            this.model = model;
        }

        /// <summary>
        /// Color propety- change the screen coler by our logic
        /// </summary>
        public string BackGroundColor
        {
            get
            {
                if (line != "" && !okClicked)
                {
                    return ("Pink");
                }
                else
                {
                    return ("White");
                }

            }
            set { }
        }

        /// <summary>
        /// Set the property of line to be the value that i get from the Command Box
        /// </summary>
        public string Line
        {
            get
            {
                NotifyPropertyChanged("BackGroundColor");
                return line;
            }
            set
            {
                line = value;
                okClicked = false;
            }
        }

        /// <summary>
        /// using the ICommand member to return if the user used the button
        /// Ganerate the Click Method
        /// </summary>
        public ICommand OkCommand
        {
            get
            {
                return this.m_OkCommand ?? (m_OkCommand = new CommandHandler(() => OkClick()));
            }
        }
        public void OkClick()
        {
            this.model.SendCommands(line);
            okClicked = true;
            NotifyPropertyChanged("BackGroundColor");
        }

        /// <summary>
        /// using the ICommand member to return if the user used the button
        /// Ganerate the Click Method
        /// </summary>
        public ICommand ClearCommand
        {
            get
            {
                return this.m_ClearCommand ?? (m_ClearCommand = new CommandHandler(() => ClearClick()));
            }
        }

        /// <summary>
        /// After the user clicked on "clear" - we send the new string to BaseNotify
        /// </summary>
        public void ClearClick()
        {
            line = "";
            NotifyPropertyChanged(line);
        }
    }
}
