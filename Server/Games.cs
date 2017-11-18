using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Server
{
    class Games
    {
        private AllPlayers players;
        public CardPacket cards;
        private bool isEnd = false;
        private Dictionary<int, Card> table;

        public Games(AllPlayers _allPlayers)
        {
            players = _allPlayers;
            cards = new CardPacket();
        }

        public void startGames()
        {
            cards.mix();
            for (int i = 0; i < players.All.Count; i++)
            {
                players.All[i].Cards = CardsPack.Distribute(8);
            }
            plate();
        }

        public void plate()
        {
            int id_start = 0;
            bool loop;
            int point;
            players.sendAll("Game start.");
            while (!isEnd)
            {

                loop = true;
                point = 0;

                while (loop)
                {
                    table = new Dictionary<int, Card>();
                    Players.findPlayer(0).HavePlay = false;
                    Players.findPlayer(1).HavePlay = false;
                    if (id_start == 0)
                    {
                        Players.findPlayer(0).sendMessage("Response", "It's your turn");
                        Players.findPlayer(1).sendMessage("Response", "Please wait");
                        while (!Players.findPlayer(0).HavePlay) ;
                        Players.findPlayer(1).sendMessage("Response", "It's your turn");
                        Players.findPlayer(0).sendMessage("Response", "Please wait");
                        while (!Players.findPlayer(1).HavePlay) ;
                    }

                    if (id_start == 1)
                    {
                        Players.findPlayer(1).sendMessage("Response", "It's your turn");
                        Players.findPlayer(0).sendMessage("Response", "Please wait");
                        while (!Players.findPlayer(1).HavePlay) ;
                        Players.findPlayer(0).sendMessage("Response", "It's your turn");
                        Players.findPlayer(1).sendMessage("Response", "Please wait");
                        while (!Players.findPlayer(0).HavePlay) ;
                    }

                    Players.sendAll(Players.findPlayer(0).Name + " a posee: " + table[0].getCard());
                    Players.sendAll(Players.findPlayer(1).Name + " a posee: " + table[1].getCard());

                    if (table[0].Force == table[1].Force)
                    {
                        point += table[0].Force + table[1].Force;
                        Players.sendAll("It's fighting time !!!! Play again");
                    }
                    if (table[0].Force > table[1].Force)
                    {
                        point += table[0].Force + table[1].Force;
                        Players.findPlayer(0).Score += point;
                        Players.sendAll(Players.findPlayer(0).Name + " you won this round ... Next round");
                        id_start = 0;
                        if (Players.findPlayer(0).Score >= 100)
                        {
                            Players.sendAll(Players.findPlayer(0).Name + " you win this game !!");
                            isEnd = true;
                        }
                        loop = false;
                    }

                    if (table[1].Force > table[0].Force)
                    {
                        point += table[0].Force + table[1].Force;
                        Players.findPlayer(1).Score += point;
                        Players.sendAll(Players.findPlayer(1).Name + " you won this round ... Next round");
                        id_start = 1;
                        if (Players.findPlayer(1).Score >= 100)
                        {
                            Players.sendAll(Players.findPlayer(1).Name + " you win this game !!");
                            isEnd = true;
                        }
                        loop = false;
                    }
                    CardsPack.PutInPacket(table[0]);
                    CardsPack.PutInPacket(table[1]);
                }
            }
        }

        public void playCard(Player player, string msg)
        {
            int number;
            if (msg.Split(' ')[1] != null)
            {
                number = int.Parse(msg.Split(' ')[1]);
                if (number >= 0 && number <= 7)
                {
                    table[player.Id] = player.pickCard(number);
                    player.CardPlayed = player.Name + " played " + table[player.Id].getCard();
                    CardsPack.AddCard(player.Cards);
                    player.HavePlay = true;
                }
                else
                {
                    player.sendMessage("Response", "Please retype command");
                }
            }
            else
            {
                player.sendMessage("Reponse", "Please retype command");
            }


        }

        internal AllPlayers Players { get => players; set => players = value; }
        internal CardPacket CardsPack { get => cards; set => cards = value; }


        internal CardPacket CardPacket
        {
            get => default(CardPacket);
            set
            {
            }
        }
    }
}
