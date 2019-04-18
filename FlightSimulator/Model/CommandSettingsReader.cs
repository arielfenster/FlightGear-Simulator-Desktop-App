using FlightSimulator.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class CommandSettingsReader
    {

        ///<summary>
        /// The Reader will parse the command
        /// Manage the command in a Dictonary-will have or commands
        /// Will send the commands to the simulator
        /// </summary>
        

        ///<summary>
        ///Parse the line we get
        /// </summary>
        public string[] Parser(string line)
        {
            string[] newLine = { "\r\n" };
            string[] input = line.Split(newLine, StringSplitOptions.None);
            return input;
        }

        /// <summary>
        /// senr the massege to the simulator
        /// </summary>
        public void SendMassaage()
        {
            
        }      

        

    }
}
