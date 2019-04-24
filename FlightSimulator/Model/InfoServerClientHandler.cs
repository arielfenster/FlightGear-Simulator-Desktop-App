//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using FlightSimulator.ViewModels;
//using FlightSimulator.Servers;

//namespace FlightSimulator.Model
//{
//    class InfoServerClientHandler : BaseNotify, IClientHandler
//    {
//        public string HandleClient(TcpClient client)
//        {
//            string data = null;
//            BinaryReader reader = new BinaryReader(client.GetStream());
//            char c = reader.ReadChar();
//            while (c != '\n' && c != '\0')
//            {
//                data += c;
//                c = reader.ReadChar();
//            }
//            return data;
//        }
//    }
//}
