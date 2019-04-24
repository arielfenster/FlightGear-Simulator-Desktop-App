using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Servers
{
    /// <summary>
    /// An interface used to define the functions of a server.
    /// </summary>
    interface IServer
    {
        /// <summary>
        /// Establish a connection to a specific socket.
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        void Connect(ISettingsModel settings);

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        void Close();

        /// <summary>
        /// Return the client that is connected to the server.
        /// </summary>
        /// <returns></returns>
        //TcpClient GetClient();


        string HandleCurrentClient();
    }
}
