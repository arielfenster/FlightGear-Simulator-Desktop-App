using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private double m_lon;
        private double m_lat;

        public double Lon
        {
            get
            {
                return m_lon;
            }
            set
            {
                m_lon = value;
                this.NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get
            {
                return m_lat;
            }
            set
            {
                m_lat = value;
                this.NotifyPropertyChanged("Lat");
            }
        }
    }
}
