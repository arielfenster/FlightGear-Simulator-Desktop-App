using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using FlightSimulator.ViewModels;
using FlightSimulator.Servers;

namespace FlightSimulator.Model
{
    /// <summary>
    /// The class receives data from the flight simulator, parses them and updates the view model to display the path of the plane
    /// </summary>
    class FlightBoardModel : BaseNotify
    {
        private float lon;
        private float lat;
        private enum Values { Lon = 0, Lat };
        private bool shouldStop;

        /// <summary>
        /// Constructor
        /// </summary>
        public FlightBoardModel()
        {
            this.shouldStop = false;
        }

        /// <summary>
        /// Longitude property function. Once the value is updated, raise a notification about it.
        /// </summary>
        public float Lon
        {
            get { return lon; }
            private set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        /// <summary>
        /// Latitude property function. Once the value is updated, raise a notification about it.
        /// </summary>
        public float Lat
        {
            get { return lat; }
            private set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        /// <summary>
        /// Start a connection as a server end point to the simulator. The simulator will conenct as a client and send data.
        /// The function will process the received data, parse it and update the values, all in a separate thread.
        /// </summary>
        /// <param name="server"> The server to connect to </param>
        public void ConnectToServer(IServer server)
        {
            server.Connect(new ApplicationSettingsModel());

            string[] lonLatVals = { "", "" };
            StreamReader reader = new StreamReader(server.GetClient().GetStream());

            shouldStop = false;
            Thread thread = null;
            thread = new Thread(() =>
            {
                while (!shouldStop)
                {
                    // Receiving the raw data from the simulator
                    string dataReceived = this.ReadFromSimulator(reader);
                    if (dataReceived == null) continue;

                    // Processing the received input to extract only the latitude and longitude values
                    this.RetrieveLonAndLat(ref dataReceived, lonLatVals);
                    // Send the new values to the flight board view model to display them
                    if (lonLatVals[(int)Values.Lon] != "")
                    {
                        this.Lon = float.Parse(lonLatVals[(int)Values.Lon]);
                    }
                    if (lonLatVals[(int)Values.Lat] != "")
                    {
                        this.Lat = float.Parse(lonLatVals[(int)Values.Lat]);
                    }
                }
                if (shouldStop)
                {
                    thread.Abort();
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Read data from the simulator.
        /// </summary>
        /// <param name="reader"> A reader object to read with </param>
        /// <returns> A raw string data from the simulator </returns>
        public string ReadFromSimulator(StreamReader reader)
        {
            string data = null;
            try
            {
                data = reader.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
            }
            return data;
        }

        /// <summary>
        /// Process an input from the flight simulator to extract the specific longitude and latitude values
        /// and store them in an array.
        /// </summary>
        /// <param name="data"> A string of raw data from the simulator. </param>
        /// <param name="details"> An array that stores the extracted values </param>
        private void RetrieveLonAndLat(ref string data, string[] details)
        {
            char[] delimiter = { ',' };
            string[] tokens = data.Split(delimiter);
            details[(int)Values.Lat] = tokens[(int)Values.Lat];
            details[(int)Values.Lon] = tokens[(int)Values.Lon];
        }

        /// <summary>
        /// Changing the flag value in order to stop the communication with the simulator.
        /// </summary>
        public void Stop()
        {
            shouldStop = true;
        }
    }
}
