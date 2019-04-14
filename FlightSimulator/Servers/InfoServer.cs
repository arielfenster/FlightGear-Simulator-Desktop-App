using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System.Net.Sockets;

namespace FlightSimulator.Servers
{
    /// <summary>
    /// A server used to receive data from the flight simulator and send it to the analyzing classes.
    /// </summary>
    class InfoServer : IServer
    {
        private TcpListener m_server;
        private FlightBoardViewModel m_flightBoardVM;
        private enum Variables { Lon = 0, Lat };

        public InfoServer(FlightBoardViewModel flightBoardVM)
        {
            m_server = null;
            m_flightBoardVM = flightBoardVM;
        }

        /// <summary>
        /// Establish a connection to a specific socket
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        public void Connect(ApplicationSettingsModel settings)
        {
            String IP = settings.FlightServerIP;
            int port = settings.FlightInfoPort;
            m_server = new TcpListener(System.Net.IPAddress.Parse(IP), port);
            m_server.Start();

            // Used to read the input from the flight simulator
            Byte[] bytes = new byte[256];

            while (true)
            {
                TcpClient client = m_server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                String msgReceived = null;

                // Read the amount of actual bytes read
                int amountOfBytes = stream.Read(bytes, 0, bytes.Length);
                while (amountOfBytes != 0)
                {
                    msgReceived = System.Text.Encoding.ASCII.GetString(bytes, 0, amountOfBytes);
                    String[] details = { "", "" };

                    // Processing the received input to receive only the latitude and longitude values
                    this.RetrieveLonAndLat(ref msgReceived, ref details);

                    // Send the new values to the flight board view model to display them
                    if (details[(int)Variables.Lon] != "")
                    {
                        m_flightBoardVM.Lon = Double.Parse(details[(int)Variables.Lon]);
                        m_flightBoardVM.NotifyPropertyChanged("Lon");
                    }
                    if (details[(int)Variables.Lat] != "")
                    {
                        m_flightBoardVM.Lat = Double.Parse(details[(int)Variables.Lat]);
                        m_flightBoardVM.NotifyPropertyChanged("Lat");
                    }
                    amountOfBytes = stream.Read(bytes, 0, bytes.Length);
                }
            }

        }

        /// <summary>
        /// Processing an input from the flight simulator to extract the specific longitude and latitude values
        /// and storing them in an array.
        /// </summary>
        /// <param name="received"> A string of raw data from the simulator. </param>
        /// <param name="details"> An array that stores the extracted values </param>
        private void RetrieveLonAndLat(ref String received, ref String[] details)
        {

        }

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        public void Close()
        {
            m_server.Stop();
        }
    }
}
