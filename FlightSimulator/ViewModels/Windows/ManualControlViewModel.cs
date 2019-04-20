using FlightSimulator.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualControlViewModel : BaseNotify
       
    {
        private CommandClient client = new CommandClient();
        /// <commands>
        /// Set the value you get from the joy-stick to the simulator adress
        /// <commands>
        public float throttelCommand
        {
            set
            {
                string command = "set controls/engines/current-engine/throttle";
                command = command + value;
                command = "\r\n";//else the simulator woudnt do nothing
                client.WriteMsg(command);
                
            }
        }
        /// <commands>
        /// Set the value you get from the joy-stick to the simulator address
        /// <commands>
        public float rudderCommand
        {
            set
            {
                string command = "set /controls/flight/elevator     ";
                command = command + value;
                command = "\r\n";//else the simulator woudnt do nothing
                client.WriteMsg(command);


            }
        }
        /// <commands>
        /// Set the value you get from the joy-stick to the simulator address
        /// <commands>
        public float elevatorCommand
        {
            set
            {
                string command = " set bla bla";
                command = command + value;
                command = "\r\n";//else the simulator woudnt do nothing
                client.WriteMsg(command);

            }
        }
        /// <commands
        /// Set the value you get from the joy-stick to the simulator address
        /// <commands>
        public float ailronCommand
        {
            set
            {
                string command = "set /controls/flight/aileron";
                command = command + value;
                command = "\r\n"; //else the simulator wodnt do nothing
                client.WriteMsg(command);
            }
        }
    }
}
