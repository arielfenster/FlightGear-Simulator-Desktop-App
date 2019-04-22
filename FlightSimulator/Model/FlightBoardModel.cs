using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using FlightSimulator.ViewModels;

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

        public void ReadDataFromClient(TcpClient client)
        {
            // Used to read the input from the flight simulator
            Byte[] bytes = new byte[256];

            NetworkStream stream = client.GetStream();

            // Read the data from the simulator
            int bytesRead = stream.Read(bytes, 0, bytes.Length);

            while (bytesRead != 0)
            {
                string dataReceived = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                string[] lonLatVals = { "", "" };

                // Processing the received input to extract only the latitude and longitude values
                this.RetrieveLonAndLat(ref dataReceived, lonLatVals);

                // Send the new values to the flight board view model to display them
                if (lonLatVals[(int)Values.Lon] != "")
                {
                    this.lon = Double.Parse(lonLatVals[(int)Values.Lon]);
                    
                }
                if (lonLatVals[(int)Values.Lat] != "")
                {
                    this.lat = Double.Parse(lonLatVals[(int)Values.Lat]);
                }
                // Continue reading
                bytesRead = stream.Read(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// Process an input from the flight simulator to extract the specific longitude and latitude values
        /// and store them in an array.
        /// </summary>
        /// <param name="received"> A string of raw data from the simulator. </param>
        /// <param name="details"> An array that stores the extracted values </param>
        private void RetrieveLonAndLat(ref string received, string[] details)
        {
            // Lon is index 0. Lat is index 1
        }
    }
}
