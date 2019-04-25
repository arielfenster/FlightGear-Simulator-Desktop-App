using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.ViewModels;
using FlightSimulator.Servers;

namespace FlightSimulator.Model
{
    /// <summary>
    /// The class's purpose is to combine similar code and logic between its deriving classes, namely ManualControlModel and AutoControlModel.
    /// Both of these classes have similar functionality with minor differences between them.
    /// </summary>
    abstract class CommandsParser : BaseNotify
    {
        private readonly CommandsServer server;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="server"> The server to which the commands are sent </param>
        public CommandsParser(CommandsServer server)
        {
            this.server = server;
        }

        
        /// <summary>
        /// Analize the string i get from the view
        /// Send in throw client chanel to the simuletor
        /// </summary>
        /// <param name="line"="line"></param>
        public void AnalyzeAndSend(string line)
        {
            if (this.server.GetClient() == null) return;

            StreamWriter st = new StreamWriter(this.server.GetClient().GetStream());

            string[] splitedLines = Parser(line);
            foreach(string splitLine in splitedLines)
            {
                string command = splitLine;
                command += "\r\n";
                st.Write(command);
                st.Flush();
            }
        }

        /// <summary>
        /// Parse the text with \r\n delimiter
        /// </summary>
        /// <param name="line"></param>
        /// <returns> input </returns>
        public string[] Parser(string line)
        {
            string[] delimiter = { "\r\n" };
            string[] input = line.Split(delimiter, StringSplitOptions.None);
            return input;
        }
    }
}
