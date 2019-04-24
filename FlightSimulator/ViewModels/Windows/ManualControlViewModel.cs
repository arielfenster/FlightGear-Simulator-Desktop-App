using FlightSimulator.Servers;
using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualControlViewModel : BaseNotify 
    {
        //private readonly CommandsServerClientHandler reader;
        private readonly ManualControlModel model;

        public ManualControlViewModel(ManualControlModel model)
        {
            //this.reader = new CommandsServerClientHandler(CommandsServer.Instance);
            this.model = model;
        }

        /// <commands>
        /// Set the value you get from the joy-stick to the simulator adress
        /// <commands>        
        public float ThrottleChange
        {
            set
            {
                this.model.SendCommand("throttle", value);
            }
        }

        /// <commands>
        /// Set the value you get from the joy-stick to the simulator address
        /// <commands>
        public float RudderChanged
        {
            set
            {
                this.model.SendCommand("rudder", value);
            }
        }
        /// <commands>
        /// Set the value you get from the joy-stick to the simulator address
        /// <commands>
        public float ElevetorCommand
        {
            set
            {
                this.model.SendCommand("elevator", value);
            }
        }
        /// <commands
        /// Set the value you get from the joy-stick to the simulator address
        /// <commands>
        public float AilronCommand
        {
            set
            {
                this.model.SendCommand("aileron", value);
            }
        }
    }
}
