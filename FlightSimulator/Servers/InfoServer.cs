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
using System.IO;

namespace FlightSimulator.Servers
{
    /// <summary>
    /// A server used to receive data from the flight simulator and send it to the analyzing classes.
    /// </summary>
    class InfoServer : IServer
    {
        private TcpListener server;
        private TcpClient client;
        private StreamReader reader;

        #region Singleton
        private static InfoServer m_instance;
        public static InfoServer Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new InfoServer();
                }
                return m_instance;
            }
        }
        private InfoServer()
        {
            this.server = null;
            this.client = null;
        }
        #endregion

        /// <summary>
        /// Establish a connection to a specific socket.
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        public void Connect(ISettingsModel settings)
        {
            try
            {
                string ip = settings.FlightServerIP;
                int port = settings.FlightInfoPort;
                this.server = new TcpListener(System.Net.IPAddress.Parse(ip), port);

                Console.WriteLine("Waiting for connection...");
                this.server.Start();
                this.client = this.server.AcceptTcpClient();
                this.reader = new StreamReader(this.client.GetStream());
                Console.WriteLine("Connected to Info server");
            }

            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: {0}", e);
            }
            catch (Exception f)
            {
                Console.WriteLine("Exception: {0}", f);
            }
        }
        
        /// <summary>
        /// Returns the current client connect.
        /// </summary>
        /// <returns> Current client connected </returns>
        public TcpClient GetClient()
        {
            return this.client;
        }

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        public void Close()
        {
            this.reader.Close();
            this.client.Close();
            this.server.Stop();
        }
    }
}
