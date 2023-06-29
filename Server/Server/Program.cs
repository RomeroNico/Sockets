using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server("localhost", 6400);
            s.Start();
        }
    }
}
