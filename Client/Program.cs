using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        internal ClientConnection ClientConnection
        {
            get => default(ClientConnection);
            set
            {
            }
        }

        internal Splash Splash
        {
            get => default(Splash);
            set
            {
            }
        }

        static void Main(string[] args)
        {
            string  ip;
            int port;
            ClientConnection client;
            string msg;

            Splash.start();
            Console.WriteLine("Welcome to our game client.");
            Console.WriteLine("Please enter the server ip:");
            ip = Console.ReadLine();
            Console.WriteLine("Please enter the server port:");
            port = int.Parse(Console.ReadLine());

            client = new ClientConnection(ip, port);
            while (!client.IsOk) ;
            client.createPlayer();
            while(true)
            {
                msg = Console.ReadLine();
                if (msg.Equals("Exit"))
                {
                    client.SendMessage(msg);
                    break;
                }
                client.SendMessage(msg);

            }
            client.cut();
            System.Environment.Exit(0);
        }
    }
}
