using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Servers;
using FlightSimulator.Model;

namespace FlightSimulator.Servers
{
    class ConnectionsManager
    {
        public void CreateConnections()
        {
            FlightBoardModel flightModel = new FlightBoardModel();
            flightModel.ConnectToServer(InfoServer.Instance);

            CommandsServer commands = CommandsServer.Instance;
            commands.Connect(ApplicationSettingsModel.Instance);
        }
    }
}
