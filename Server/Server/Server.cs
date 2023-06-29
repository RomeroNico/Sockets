using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Server
    {
        IPHostEntry host;
        IPAddress ipAddr;
        IPEndPoint endPoint;

        Socket s_Server;
        Socket s_Client;

        public Server(string ip, int port)
        {
            host = Dns.GetHostEntry(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);

            s_Server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            s_Server.Bind(endPoint);
            s_Server.Listen(10);

        }

        public void Start()
        {
            Thread t;

            while (true)
            {
                Console.WriteLine("Wait...");
                s_Client = s_Server.Accept();
                t = new Thread(ClientConnection);
                t.Start(s_Client);
                Console.WriteLine("Client Connect!");
                Console.Out.Flush();
            }
        }

        public void ClientConnection(object s)
        {
            Socket s_Cliente = (Socket)s;
            byte[] buffer;
            string message;

            try
            {
                while (true)
                {
                    buffer = new byte[1024];
                    s_Client.Receive(buffer);
                    message = byteToString(buffer);
                    Console.WriteLine($">>> {message}");
                }

            }
            catch (SocketException se)
            {
                Console.WriteLine($"*****    {se.Message}    *****");
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
