using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class AllPlayers
    {
        private List<Player> all;
        private int nbrPlayer = 0;

        public AllPlayers()
        {
            all = new List<Player>();
        }

        public int NbrPlayer { get => nbrPlayer; set => nbrPlayer = value; }
        internal List<Player> All { get => all; set => all = value; }

        internal Player Player
        {
            get => default(Player);
            set
            {
            }
        }

        public void addPlayer(Player player)
        {
            Console.WriteLine("Adding Player...");
            all.Add(player);
            Console.WriteLine("Player added");
        }

        public Player findPlayer(Connection c)
        {
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Connection == c)
                {
                    return all[i];
                }
            }
            return null;
        }

        public Player findPlayer(string name)
        {
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Name == name)
                {
                    return all[i];
                }
            }
            return null;
        }

        public Player findPlayer(int id)
        {
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == id)
                {
                    return All[i];
                }
            }

            return null;
        }

        public void sendAll(string msg)
        {
            for (int i = 0; i < all.Count; i++)
            {
                all[i].sendMessage("data",msg);
            }
        }
    }
}
