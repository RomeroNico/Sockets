using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client("localhost", 6400);
            string msg;
            c.Start();
            while (true)
            {
                Console.Write(">>> ");
                msg = Console.ReadLine();
                c.Send(msg);
            }
        }
    }
}
