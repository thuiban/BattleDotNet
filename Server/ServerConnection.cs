using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Cards;

namespace Server
{
    class ServerConnection
    {
        private int port;
        public AllPlayers players;
        public bool start_game = false;
        private Games game;

        public Games Game { get => game; set => game = value; }
        public AllPlayers Players { get => players; set => players = value; }
        public bool Start_game { get => start_game; set => start_game = value; }

        internal AllPlayers AllPlayers
        {
            get => default(AllPlayers);
            set
            {
            }
        }
       

        internal CardPacket CardPacket
        {
            get => default(CardPacket);
            set
            {
            }
        }

        internal Games Games
        {
            get => default(Games);
            set
            {
            }
        }

        public ServerConnection(int _port)
        {
            port = _port;
            Console.WriteLine("Starting connection to server.");
            players = new AllPlayers();
            Game = new Games(players);
            StartConnection();
        }

        public void StartConnection()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("NewPlayer", AddPlayer);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Request", PrintIncomingMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("TestConnection", TestConnection);
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));
            Console.WriteLine("Server listening for TCP connection on:");
            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
                Console.WriteLine("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
        }

        public void wait()
        {
            while(!Start_game)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void ServerStop()
        {
            NetworkComms.Shutdown();
            Console.WriteLine("Server stopped.");
            Environment.Exit(0);
        }

        private void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            if (message.Equals("Exit"))
            {
                ServerStop();
            }
            if (Start_game)
            {
                string cmd = message.Split(' ')[0];
                switch (cmd)
                {
                    case "HELP":
                        string help = "Command:\n";
                        help = help + "HELP  display this message\n";
                        help = help + "CARDS display cards\n";
                        help = help + "SCORE display your score\n";
                        help = help + "PLAY {Number of Card} play one card";
                        Players.findPlayer(connection).sendMessage("Response", help);
                        break;
                    case "CARDS":
                        players.findPlayer(connection).sendMessage("Response", players.findPlayer(connection).getCards());
                        break;
                    case "SCORE":
                        players.findPlayer(connection).sendMessage("Response", players.findPlayer(connection).getScore());
                        break;
                    case "PLAY":
                        Game.playCard(players.findPlayer(connection), message);
                        break;
                    default:
                        Players.findPlayer(connection).sendMessage("Response", "Command not found. Try HELP");
                        break;
                }
            }
            else
            {
                Players.findPlayer(connection).sendMessage("Response", "Wait game please.");
            }
        }

        private  void AddPlayer(PacketHeader header, Connection connection, string name)
        {

            Console.WriteLine("i'm here");
            Players.addPlayer(new Player(name, connection, Players.NbrPlayer));
            Console.WriteLine("Player has joined the game");
            Players.NbrPlayer++;
            Console.WriteLine("Player has joined the game");
            Players.findPlayer(connection).sendMessage("Response", "You have joined the party");
            Console.WriteLine("Player has joined the game");
            if (Players.All.Count < 2)
            {
                Players.sendAll(name + " has joined the game. We are waiting for " + (2 - Players.All.Count) + " player");
            }
            else
            {
                Players.sendAll(name + " has joined the game. We will start soon...");
                Start_game = true;
            }
            Console.WriteLine("Player has joined the game");
        }

        private void TestConnection(PacketHeader header, Connection connection, string msg)
        {
            Console.WriteLine("Receive Connection");
            connection.SendObject("Response", "You are connected");
        }

        public void startGame()
        {
            Game.startGames();
        }
    }
}
