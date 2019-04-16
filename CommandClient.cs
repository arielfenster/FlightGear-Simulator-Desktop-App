using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class CommandClient : ICommandClient
    {
        private int port;
        private string ip;
        private IPEndPoint endPoint;
        private TcpClient tcpClient;

        // here i init the members for the connection
        public CommandClient(int port, string ip)
        {
            this.port = port;
            this.ip = ip;
        }
        
        // Connect method:
        // First i get the ip address
        // After this i connect by TCP protocol
        
        public void connect()
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(ipAddress, port);
            tcpClient = new TcpClient();
            tcpClient.Connect(endPoint);
            Console.WriteLine("Connected");
        }
        //Dissconnect as TCP client
        public void disconnect()
        {
            tcpClient.Close();
        }
        // Write the massage to the simulator
        // Use StreamWrite to write the simuletur
        public void writeMsg(string msg)
        {

            NetworkStream ns = tcpClient.GetStream();
            StreamWriter st = new StreamWriter(ns);
            st.Write(msg);
            st.Flush();// very important


        }
    }
}
