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

        #region Singleton
        private static FlightBoardModel m_instance;
        public static FlightBoardModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new FlightBoardModel();
                }
                return m_instance;
            }
        }
        #endregion
        public float Lon
        {
            get { return this.lon; }
            private set
            {
                this.lon = value;
                this.NotifyPropertyChanged("Lon");
            }
        }
        public float Lat
        {
            get { return this.lat; }
            private set
            {
                this.lat = value;
                this.NotifyPropertyChanged("Lat");
            }
        }

        public void ConnectToServer(InfoServer server)
        {
            server.Connect(new ApplicationSettingsModel());
            string[] lonLatVals = { "", "" };

            Thread thread = new Thread(() =>
            {
                while (true)
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
    }
}
