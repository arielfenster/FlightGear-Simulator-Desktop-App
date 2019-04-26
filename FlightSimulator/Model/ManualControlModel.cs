using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Servers;

namespace FlightSimulator.Model
{
    /// <summary>
    /// The class receives commands from the view model, converts them to a specific format and sends them to the simulator.
    /// </summary>
    class ManualControlModel : CommandsParser
    {
        private Dictionary<string, string> commandsNamesPaths;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server"> The server to which the commands will be sent </param>
        public ManualControlModel(CommandsServer server) : base(server)
        {
            this.commandsNamesPaths = new Dictionary<string, string>
            {
                        { "THROTTLE", "/controls/engines/current-engine/throttle" },
                        { "RUDDER", "/controls/flight/rudder" },
                        { "ELEVATOR", "/controls/flight/elevator" },
                        { "AILERON", "/controls/flight/aileron" }
            };
        }
        
        /// <summary>
        /// Creates a string command based on the given name and value and sends the command to the simulator.
        /// </summary>
        /// <param name="name"> The name of the property </param>
        /// <param name="value"> The value of the property </param>
        public void SendCommand(string name, float value)
        {
            string command = "set " + this.commandsNamesPaths[name.ToUpper()] + " " + value + "\r\n";
            this.AnalyzeAndSend(command);
        }
    }
}
