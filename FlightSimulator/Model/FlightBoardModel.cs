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
        private double lon;
        private double lat;
        private enum Values { Lon = 0, Lat };

        public double Lon
        {
            get { return this.lon; }
            set
            {
                this.lon = value;
                this.NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get { return this.lat; }
            set
            {
                this.lat = value;
                this.NotifyPropertyChanged("Lat");
            }
        }

        public void ConnectToServer(InfoServer server)
        {
            server.Connect(new ApplicationSettingsModel());
            Thread thread = new Thread(delegate ()
            {
                while (true)
                {
                    string dataReceived = server.ReadFromSimulator();
                    string[] lonLatVals = { "", "" };

                    // Processing the received input to extract only the latitude and longitude values
                    this.RetrieveLonAndLat(ref dataReceived, lonLatVals);

                    // Send the new values to the flight board view model to display them
                    if (lonLatVals[(int)Values.Lon] != "")
                    {
                        this.Lon = Double.Parse(lonLatVals[(int)Values.Lon]);
                    }
                    if (lonLatVals[(int)Values.Lat] != "")
                    {
                        this.Lat = Double.Parse(lonLatVals[(int)Values.Lat]);
                    }
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
            string[] split = data.Split(delimiter);
            details[(int)Values.Lat] = split[(int)Values.Lat];
            details[(int)Values.Lon] = split[(int)Values.Lon];
        }
    }
}
