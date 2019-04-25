using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Servers;

namespace FlightSimulator.Model
{
    class ManualControlModel : CommandsParser
    {
        private Dictionary<string, string> commandsNamesPaths;

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
        
        public void SendCommand(string name, float value)
        {
            string command = "set " + this.commandsNamesPaths[name.ToUpper()] + " " + value + "\r\n";
            this.AnalyzeAndSend(command);
        }
    }
}
