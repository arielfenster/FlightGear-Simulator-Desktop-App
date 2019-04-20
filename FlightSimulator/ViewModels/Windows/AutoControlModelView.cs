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
        private CommandReader m_reader;

   

        public AutoControlModelView()
        {
            this.line = "";
            this.m_reader = new CommandReader();
        }

        /// <summary>
        /// Color propety- change the screen coler by our logic
        /// </summary>
        public string Color
        {
            get
            {
                if (this.line != "")
                {
                    return ("RED");
                }
                else
                {
                    return ("White");
                }

            }
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
            get
            {
                NotifyPropertyChanged("Color");
                return this.line;
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

            m_reader.AnalyzeAndSend(line);

        }

        /// <summary>
        /// using the ICommand member to return if the user used the button
        /// Ganerate the Click Method
        /// </summary>
        public ICommand ClearCommand
        {
            get
            {
                return this.m_ClearCommand ?? (m_OkCommand = new CommandHandler(() => ClearClick()));
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
