using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface ICommandClient
    {
        // the methods that i will use
        void connect();
        void disconnect();
        void writeMsg(string msg);
        

    }
}
