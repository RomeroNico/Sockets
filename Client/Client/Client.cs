using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        IPAddress ipAddr;
        IPEndPoint endPoint;

        Socket s_Client;

        public Client(string ip, int port)
        {
            ipAddr = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(ipAddr, port);

            s_Client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            s_Client.Connect(endPoint);
        }

        public void Close()
        {
            s_Client.Close();
        }

        public void Send(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            s_Client.Send(data);
        }

        public void Send(byte[] msg)
        {
            int bytesSent = s_Client.Send(msg);
            Console.WriteLine("sent {0} bytes", bytesSent);
            for (int i = 0; i < bytesSent; i++)
            {
                Console.WriteLine("{0} - {1} - {2}", i, msg[i], (char)msg[i]);
            }
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
