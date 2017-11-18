using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    public class CardPacket
    {
        private List<Card> cards = new List<Card>(32);

        public List<Card> Cards { get => cards; set => cards = value; }

        public CardPacket()
        {

            List<String> color = new List<string>(collection: new string[] { "HEART", "SPADE", "CLUB", "DIAMOND" });
            List<String> figure = new List<string>(collection: new string[] { "SEVEN", "EIGHT", "NINE", "TEN", "JACK", "QUEEN", "KING", "ACE" });
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cards.Add(new Card(color[i], figure[j], j));
                }
            }
        }


        public void mix()
        {
            Random random = new Random();
            int n = Cards.Count;
            for (int i = 0; i < n; i++)
            {
                int r = i + random.Next(n - i);
                Card tmp = Cards[r];
                Cards[r] = Cards[i];
                Cards[i] = tmp;
            }
        }

        public List<Card> Distribute(int nbr)
        {
            List<Card> dist = new List<Card>();
            for (int i = 0; i < nbr; i++)
            {
                dist.Add(Cards[0]);
                Cards.RemoveAt(0);
            }
            return dist;
        }

        public void AddCard(List<Card> list)
        {
            list.Add(Cards[0]);
            Cards.RemoveAt(0);
        }

        public void PutInPacket(Card card)
        {
            Cards.Add(card);
        }
    }
}
