using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        IPHostEntry host;
        IPAddress ipAddr;
        IPEndPoint endPoint;

        Socket s_Client;

        public Client(string ip, int port)
        {
            host = Dns.GetHostEntry(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);

            s_Client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            s_Client.Connect(endPoint);
        }

        public void Send(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            s_Client.Send(data);
        }

        public string byteToString(byte[] buffer)
        {
            string message;
            int endIndex;

            message = Encoding.ASCII.GetString(buffer);
            endIndex = message.IndexOf('\0');
            if (endIndex > 0)
            {
                message = message.Substring(0, endIndex);
            }
            return message;
        }
    }
}
