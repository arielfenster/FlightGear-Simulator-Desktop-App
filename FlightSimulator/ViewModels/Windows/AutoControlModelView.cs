using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    class AutoControlModelView:BaseNotify
    {
        /// <summary>
        /// Icommand - contract for commands that are written in view
        /// m_OkCommad- Icommand member for the "ok" command
        /// m_ClearCommand- Icommand member for the "clear" command
        /// 
        /// </summary>
        private string line;
        private ICommand m_OkCommand;
        private ICommand m_ClearCommand;

        public AutoControlModelView()
        {
            this.line = "";

        }

        /// <summary>
        /// Set the property of line to be the value that i get from the Command Box
        /// </summary>
        public string Line
        {
            set
            {
                line = value;
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
            //sent the line TODO 
        }

        /// <summary>
        /// using the ICommand member to return if the user used the button
        /// Ganerate the Click Method
        /// </summary>
        public ICommand ClearCommand
        {
            get
            {
                return this.m_OkCommand ?? (m_OkCommand = new CommandHandler(() => ClearClick()));
            }
        }

        /// <summary>
        /// After the user clicked on "clear" - we send the new string to BaseNotify
        /// </summary>
        public void ClearClick()
        {
            line = "" ;
            NotifyPropertyChanged(line);
        }

    }
}
