using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace Client
{
    class ClientConnection
    {
        private int port;
        private string ip;
        private bool isOk;
        public ClientConnection(string _ip, int _port)
        {
            this.ip = _ip;
            this.port = _port;
            isOk = false;
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("data", ReceiveData);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Response", ResponseMsg);
            SendTest();
        }

        public bool IsOk { get => isOk; set => isOk = value; }

        public void SendTest()
        {
            try
            {
                NetworkComms.SendObject<string>("TestConnection", ip, port, "Test connection");
            }
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            catch (ConnectionSetupException e)
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
            {
                Console.WriteLine("Can't connect to server");
                Console.WriteLine("Client is down. Please press a key to stop the client");
                Console.ReadKey(true);
                Environment.Exit(2);
            }
        }
        public bool SendMessage(string msg)
        {
            try
            {
                NetworkComms.SendObject<string>("Request", ip, port, msg);
            }
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            catch (ConnectionSetupException e)
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
            {
                Console.WriteLine("Can't connect to server");
                Console.WriteLine("Client is down. Please press a key to stop the client");
                Console.ReadKey(true);
                Environment.Exit(2);
            }
            return true;
        }

      
        public void cut()
         {
             NetworkComms.Shutdown();
         }

        private void ReceiveData(PacketHeader header, Connection connection, string msg)
        {
            if (msg.Equals("EXIT"))
            {
                cut();
                Console.WriteLine("Server is down. Please press a key to stop the client");
                Console.ReadKey(true);
                Environment.Exit(0);
            }

            Console.WriteLine("[Server Message]: " + msg);
        }

        public void createPlayer() 
        {
            string name;
            Console.Write("Your name: ");
            name = Console.ReadLine();
            try
            {
                NetworkComms.SendObject<string>("NewPlayer", ip, port, name);
            }
            catch (ConnectionSetupException e)
            {
                Console.WriteLine("Can't connect to server");
                Console.WriteLine("Client is down. Please press a key to stop the client");
                Console.ReadKey(true);
                Environment.Exit(2);
            }
        }

        public void ResponseMsg(PacketHeader header, Connection connection, string msg)
        {
            if (msg.Equals("You are connected"))
            {
                IsOk = true;
            }
            else
            {
                Console.WriteLine(msg);
            }
        }
    }

    
}
