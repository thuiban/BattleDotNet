using System;

namespace Cards
{
    public class Card
    {
        private string color;
        private string figure;
        private int force;

        public Card(string _color, string _figure, int _force)
        {
            color = _color;
            figure = _figure;
            Force = _force;
        }

        public int Force { get => force; set => force = value; }

        public string getCard()
        {
            return figure + " of " + color;
        }
    }
}
