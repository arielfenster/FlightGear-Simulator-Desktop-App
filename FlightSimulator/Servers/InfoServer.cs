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
        private BinaryReader reader;

        //private readonly FlightBoardModel flightModel;

        public InfoServer()
        {
            this.server = null;
            this.client = null;
        }

        /// <summary>
        /// Establish a connection to a specific socket
        /// </summary>
        /// <param name="settings"> Holds the IP and port of the requested connection. </param>
        public void Connect(ISettingsModel settings)
        {
            //Thread thread = new Thread(delegate () { this.flightModel.ReadDataFromClient(client);});
            try
            {
                string ip = settings.FlightServerIP;
                int port = settings.FlightInfoPort;
                this.server = new TcpListener(System.Net.IPAddress.Parse(ip), port);
                this.server.Start();

                Console.WriteLine("Waiting for connection...");
                this.client = this.server.AcceptTcpClient();
                this.reader = new BinaryReader(this.client.GetStream());
                Console.WriteLine("Connected!");
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

        public string ReadFromSimulator()
        {
            string data = null;
            char c = reader.ReadChar();
            while (c != '\n')
            {
                data += c;
                c = reader.ReadChar();
            }
            return data;
        }

        /// <summary>
        /// Close the connection to the socket.
        /// </summary>
        public void Close()
        {
            //this.shouldStop = true;
            //this.thread.Abort();
            this.client.Close();
            this.server.Stop();
        }
    }
}


/*
 * this.thread = new Thread(delegate ()
                {
                    // Used to read the input from the flight simulator
                    Byte[] bytes = new byte[256];
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);

                    while (!this.shouldStop)
                    {
                        string dataReceived = this.ReadFromSimulator(client);



//                        string dataReceived = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        string[] lonLatVals = { "", "" };

                        // Processing the received input to extract only the latitude and longitude values
                        this.RetrieveLonAndLat(ref dataReceived, lonLatVals);

                        // Send the new values to the flight board view model to display them
                        if (lonLatVals[(int)Values.Lon] != "")
                        {
                            this.flightModel.Lon = Double.Parse(lonLatVals[(int)Values.Lon]);
                        }
                        if (lonLatVals[(int)Values.Lat] != "")
                        {
                            this.flightModel.Lat = Double.Parse(lonLatVals[(int)Values.Lat]);
                        }
                    }
                });
                thread.Start();
*/