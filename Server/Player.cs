using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cards;

namespace Server
{
    class Player
    {
        private string name;
        private Connection connection;
        private int id;
        private List<Card> cards;
        private int score = 0;
        private bool havePlay = false;
        private string cardPlayed;

        public Player(string name, Connection connection, int id)
        {
            this.Name = name;
            this.Connection = connection;
            this.Id = id;
            Cards = new List<Card>();
        }

        public string Name { get => name; set => name = value; }
        public Connection Connection { get => connection; set => connection = value; }
        public int Id { get => id; set => id = value; }
        internal List<Card> Cards { get => cards; set => cards = value; }
        public string CardPlayed { get => cardPlayed; set => cardPlayed = value; }

        internal Card Card
        {
            get => default(Card);
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

      

        public bool HavePlay { get => havePlay; set => havePlay = value; }
        public int Score { get => score; set => score = value; }

        public void sendMessage(string type, string message)
        {
            try
            {
                connection.SendObject<string>(type, message);
            }
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            catch (NetworkCommsDotNet.ConnectionSetupException e)
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée

            {
                Console.WriteLine("Can't send message to " + name);
                System.Environment.Exit(2);
            }
        }

        public string getCards()
        {
            string list_cards = "";
            for (int i = 0; i < cards.Count; i++)
            {
                list_cards = list_cards + i + " : " + cards[i].getCard() + "\n";
            }
            return (list_cards);
        }

        public Card pickCard(int number)
        {
            Card tmp;
            for (int i = 0; i < Cards.Count; i++)
            {
                if (i == number)
                {
                    tmp = Cards[i];
                    Cards.RemoveAt(i);
                    return tmp;
                }
            }

            return null;
        }

        public string getScore()
        {
            string msg = "Your score: " + Score + "\n";
            return msg;
        }
    }
}
