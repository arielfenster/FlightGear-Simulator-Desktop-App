using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Servers
{
    public class CommandsServer : IServer
    {
     //   private int port;
     //   private string ip;
        private IPEndPoint endPoint;
        private TcpClient tcpClient;
        public static CommandsServer instance;

        #region Singelton
        public CommandsServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommandsServer();
                }
                return instance;
            }
        }
        // here i init the members for the connection
        private CommandsServer()
        {
            this.endPoint = null;
            this.tcpClient = null;
        }
        #endregion

        // Connect method:
        // First i get the ip address
        // After this i connect by TCP protocol

        public void Connect(ISettingsModel settings)
        {
            IPAddress ipAddress = IPAddress.Parse(settings.FlightServerIP);
            this.endPoint = new IPEndPoint(ipAddress, settings.FlightCommandPort);
            this.tcpClient = new TcpClient();
            this.tcpClient.Connect(endPoint);
            Console.WriteLine("Connected");
        }
        //Dissconnect as TCP client
        public void Close()
        {
            tcpClient.Close();
        }
        // Write the massage to the simulator
        // Use StreamWrite to write the simuletur
        public void WriteMsg(string msg)
        {
            NetworkStream ns = tcpClient.GetStream();
            StreamWriter st = new StreamWriter(ns);
            st.Write(msg);
            st.Flush();// very important

            


        }
    }
}
