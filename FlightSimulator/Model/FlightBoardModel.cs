using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using FlightSimulator.ViewModels;
using FlightSimulator.Servers;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    class FlightBoardModel : BaseNotify
    {
        private float lon;
        private float lat;
        private enum Values { Lon = 0, Lat };
        private bool shouldStop;
        private Thread thread;

        public FlightBoardModel()
        {
            this.shouldStop = false;
            this.thread = null;
        }

        public float Lon
        {
            get { return lon; }
            private set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }
        public float Lat
        {
            get { return lat; }
            private set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        public void ConnectToServer(InfoServer server)
        {
            server.Connect(new ApplicationSettingsModel());
            string[] lonLatVals = { "", "" };

            thread = new Thread(() =>
            {
                while (!shouldStop)
                {
                    // Receiving the raw data from the simulator
                    string dataReceived = server.ReadFromSimulator();
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

        public void Stop()
        {
            shouldStop = true;
        }
    }
}
