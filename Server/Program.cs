using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        internal ServerConnection ServerConnection
        {
            get => default(ServerConnection);
            set
            {
            }
        }

        static void Main(string[] args)
        {
            int port;
            ServerConnection server;
            Thread waitPlayer;

            Console.WriteLine("Please enter the port:");
            port = int.Parse(Console.ReadLine());
            server = new ServerConnection(port);
            waitPlayer = new Thread(server.wait);
            waitPlayer.Start();
            while (!waitPlayer.IsAlive) ;
            waitPlayer.Join();
            server.startGame();
            Console.ReadKey(true);
            server.ServerStop();
            
        }
    }
}
