using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.ViewModels;
using FlightSimulator.Servers;
using System.Net.Sockets;

namespace FlightSimulator.Model
{
    /// <summary>
    /// The class's purpose is to send, commands to simulator after analyzing(parse them)
    /// Have 2 sec break between each command
    ///
    /// </summary>

    class AutoControlModel : CommandsParser
    {
        public AutoControlModel(CommandsServer server) : base(server) { }

        public void SendCommands(string commands)
        {
            // Split the text into commands split by \r\n, then send each command to the simulator in intervals of 2 seconds between each command
            string[] tokens = this.Parser(commands);
            foreach (string command in tokens)
            {
                this.AnalyzeAndSend(command);
                Thread.Sleep(2000);
            }
        }
    }
}
