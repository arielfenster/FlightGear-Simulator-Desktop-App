using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualControlModel : CommandsServerClientHandler
    {
        private Dictionary<string, string> commandsNamesPaths;

        #region Singleton
        private static ManualControlModel m_instance;
        public static ManualControlModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    // Creating an instance of the class and creating pairs of command name and its appropriate path
                    m_instance = new ManualControlModel();
                    m_instance.commandsNamesPaths = new Dictionary<string, string>
                    {
                        { "THROTTLE", "/controls/engines/current-engine/throttle" },
                        { "RUDDER", "/controls/flight/rudder" },
                        { "ELEVATOR", "/controls/flight/elevator" },
                        { "AILERON", "/controls/flight/aileron" }
                    };
                }
                return m_instance;
            }
        }
        #endregion

        public void SendCommand(string name, float value)
        {
            string command = "set " + Instance.commandsNamesPaths[name.ToUpper()] + " " + value + "\r\n";
            this.AnalyzeAndSend(command);
        }
    }
}
