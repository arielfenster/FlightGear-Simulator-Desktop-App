using FlightSimulator.Servers;
using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// The class receives commands from the view object and sends them to the analyzing model object.
    /// </summary>
    class ManualControlViewModel : BaseNotify 
    {
        private readonly ManualControlModel model;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"> Responsible for analyzing the received commands </param>
        public ManualControlViewModel(ManualControlModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Set the value you get from the joy-stick to the simulator address
        /// </summary>
        public float ThrottleChange
        {
            set
            {
                this.model.SendCommand("throttle", value);
            }
        }

        /// <summary>
        /// Set the value you get from the joy-stick to the simulator address
        /// </summary>
        public float RudderChanged
        {
            set
            {
                this.model.SendCommand("rudder", value);
            }
        }

        /// <summary>
        /// Set the value you get from the joy-stick to the simulator address
        /// </summary>
        public float ElevetorCommand
        {
            set
            {
                this.model.SendCommand("elevator", value);
            }
        }

        /// <summary>
        /// Set the value you get from the joy-stick to the simulator address
        /// </summary>
        public float AileronCommand
        {
            set
            {
                this.model.SendCommand("aileron", value);
            }
        }
    }
}
