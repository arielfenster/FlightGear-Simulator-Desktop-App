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
        private IPEndPoint endPoint;
        private TcpClient tcpClient;

        #region Singelton
        private static CommandsServer m_instance;
        public static CommandsServer Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new CommandsServer();
                }
                return m_instance;
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
            Console.WriteLine("Connected to Commands server");
        }

        //Dissconnect as TCP client
        public void Close()
        {
            tcpClient.Close();
        }

        public TcpClient GetClient()
        {
            return this.tcpClient;
        }
    }
}