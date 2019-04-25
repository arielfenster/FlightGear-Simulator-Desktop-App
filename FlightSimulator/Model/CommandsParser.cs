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
        ///<summary>
        /// The Reader will parse the command
        /// Manage the command in a Dictonary-will have or commands
        /// Will send the commands to the simulator
        /// </summary>

        /*
        public string handleclient(tcpclient client)
        {
            networkstream ns = client.getstream();
            streamwriter st = new streamwriter(ns);
            //st.write(msg);
            st.flush();// very important
            return null;
        }
        */

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
                
                //m_clientChannel.WriteMsg(command);
                //wait 2 sec between each command
                //Thread.Sleep(2000);

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
