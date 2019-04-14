using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;

namespace FlightSimulator.Servers
{
    /// <summary>
    /// An interface used to define the functions of a server.
    /// </summary>
    interface IServer
    {
        /// <summary>
        /// Establish a connection to a specific socket
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        void Connect(ApplicationSettingsModel settings);

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        void Close();
    }
}
