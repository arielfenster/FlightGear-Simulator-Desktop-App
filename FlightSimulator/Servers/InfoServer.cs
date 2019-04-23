using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using System.Net.Sockets;

namespace FlightSimulator.Servers
{
    /// <summary>
    /// A server used to receive data from the flight simulator and send it to the analyzing classes.
    /// </summary>
    class InfoServer : IServer
    {
        private TcpListener server;
        private readonly FlightBoardModel flightModel;

        public InfoServer(FlightBoardModel flightBoardModel)
        {
            this.server = null;
            this.flightModel = flightBoardModel;
        }

        /// <summary>
        /// Establish a connection to a specific socket
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        public void Connect(ISettingsModel settings)
        {
            TcpClient client = null;
            try
            {
                string ip = settings.FlightServerIP;
                int port = settings.FlightInfoPort;
                this.server = new TcpListener(System.Net.IPAddress.Parse(ip), port);
                this.server.Start();
                client = this.server.AcceptTcpClient();
                this.flightModel.ReadDataFromClient(client);
            }

            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: ", e);
            }
            catch (Exception f)
            {
                Console.WriteLine("Exception: ", f);
            }
            finally
            {
                client.GetStream().Close();
                client.Close();
                this.Close();
            }
        }       

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        public void Close()
        {
            this.server.Stop();
        }
    }
}
 