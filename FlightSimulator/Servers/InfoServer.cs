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
        private FlightBoardViewModel flightBoardVM;
        private enum Variables { Lon = 0, Lat };

        public InfoServer(FlightBoardViewModel flightBoardVM)
        {
            this.server = null;
            this.flightBoardVM = flightBoardVM;
        }

        /// <summary>
        /// Establish a connection to a specific socket
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        public void Connect(ISettingsModel settings)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            try
            {
                String IP = settings.FlightServerIP;
                int port = settings.FlightInfoPort;
                this.server = new TcpListener(System.Net.IPAddress.Parse(IP), port);
                this.server.Start();

                // Used to read the input from the flight simulator
                Byte[] bytes = new byte[256];

                while (true)
                {
                    client = this.server.AcceptTcpClient();
                    stream = client.GetStream();
                    String dataReceived = null;

                    // Read the data from the simulator
                    int bytesRead = stream.Read(bytes, 0, bytes.Length);
                    Console.WriteLine(bytes);
                    while (bytesRead != 0)
                    {
                        dataReceived = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        String[] lonLatVals = { "", "" };

                        // Processing the received input to receive only the latitude and longitude values
                        this.RetrieveLonAndLat(ref dataReceived, ref lonLatVals);

                        // Send the new values to the flight board view model to display them
                        if (lonLatVals[(int)Variables.Lon] != "")
                        {
                            this.flightBoardVM.Lon = Double.Parse(lonLatVals[(int)Variables.Lon]);
                        }
                        if (lonLatVals[(int)Variables.Lat] != "")
                        {
                            this.flightBoardVM.Lat = Double.Parse(lonLatVals[(int)Variables.Lat]);
                        }
                        // Continue reading
                        bytesRead = stream.Read(bytes, 0, bytes.Length);
                    }
                }
            }
            catch(SocketException e)
            {
                Console.WriteLine("Socket Exception: ", e);
            }
            finally
            {
                stream.Close();
                client.Close();
                this.Close();
                
            }
        }

        /// <summary>
        /// Process an input from the flight simulator to extract the specific longitude and latitude values
        /// and store them in an array.
        /// </summary>
        /// <param name="received"> A string of raw data from the simulator. </param>
        /// <param name="details"> An array that stores the extracted values </param>
        private void RetrieveLonAndLat(ref String received, ref String[] details)
        {
            // Lon is index 0. Lat is index 1
        }

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        public void Close()
        {
            this.server.Stop();
        }

        public static void Main(string[] args)
        {
            ISettingsModel settings = new ApplicationSettingsModel
            {
                FlightServerIP = "127.0.0.1",
                FlightInfoPort = 5400
            };
            IServer s = new InfoServer(new FlightBoardViewModel());
            Thread t = new Thread(delegate ()
            {
                s.Connect(settings);
            });
            t.Start();
            t.Join();
            Console.ReadKey();
        }
    }
}
 