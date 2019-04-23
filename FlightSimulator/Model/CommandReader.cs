using FlightSimulator.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class CommandReader
    {

        ///<summary>
        /// The Reader will parse the command
        /// Manage the command in a Dictonary-will have or commands
        /// Will send the commands to the simulator
        /// </summary>

        CommandsServer m_clientChannel;
        
        public CommandReader(CommandsServer client)
        {
            m_clientChannel = client;
        }


        /// <summary>
        /// Analize the string i get from the view
        /// Send in throw client chanel to the simuletor
        /// </summary>
        /// <param name="line"="line"></param>
        internal void AnalyzeAndSend(string line)
        {
            string[] splitedLines = Parser(line);
            foreach(string splitLine in splitedLines)
            {
                string command = splitLine;
                command += "\r\n";
                m_clientChannel.WriteMsg(command);
                //wait 2 sec between each command
                Thread.Sleep(2000);

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
