using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cards;
using System.Collections.Generic;

namespace CardsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNbrCard()
        {
            int nbr;

            CardPacket cardPacket = new CardPacket();
            nbr = cardPacket.Cards.Count;
            Assert.AreEqual<int>(32, nbr);
            Assert.AreNotEqual<int>(31, nbr);
        }

       
        [TestMethod]
        public void TestSuffle1()
        {
            CardPacket cards1 = new CardPacket();
            CardPacket cards2 = new CardPacket();

            cards1.mix();
            cards2.mix();

            Assert.AreNotEqual<List<Card>>(cards1.Cards, cards2.Cards);

        }

        [TestMethod]
        public void TestSuffle2()
        {
            CardPacket cards1 = new CardPacket();
            CardPacket cards2 = new CardPacket();

            for (int i = 0; i < 10; i++)
            {
                cards1.mix();
                cards2.mix();
            }

            Assert.AreNotEqual<List<Card>>(cards1.Cards, cards2.Cards);

        }

        [TestMethod]
        public void TestSuffle3()
        {
            CardPacket cards1 = new CardPacket();
            CardPacket cards2 = new CardPacket();

            for (int i = 0; i < 100; i++)
            {
                cards1.mix();
                cards2.mix();
            }

            Assert.AreNotEqual<List<Card>>(cards1.Cards, cards2.Cards);

        }

        [TestMethod]
        public void TestDistribute1()
        {
            CardPacket cards = new CardPacket();
            List<Card> pack1;
            List<Card> pack2;

            pack1 = cards.Distribute(8);
            pack2 = cards.Distribute(8);

            Assert.AreEqual<int>(pack1.Count, 8);
            Assert.AreEqual<int>(pack2.Count, 8);

            Assert.AreNotEqual<List<Card>>(pack1, pack2);

        }

        [TestMethod]
        public void TestDistribute2()
        {
            CardPacket cards = new CardPacket();
            List<Card> pack1;
            List<Card> pack2;

            cards.mix();
            pack1 = cards.Distribute(8);
            pack2 = cards.Distribute(8);

            Assert.AreEqual<int>(pack1.Count, 8);
            Assert.AreEqual<int>(pack2.Count, 8);

            Assert.AreNotEqual<List<Card>>(pack1, pack2);

        }

        [TestMethod]
        public void TestDistribute3()
        {
            CardPacket cards = new CardPacket();
            List<Card> pack1;
            List<Card> pack2;

            for (int i = 0; i < 10; i++)
            {
                cards.mix();
            }

            pack1 = cards.Distribute(8);
            pack2 = cards.Distribute(8);

            Assert.AreEqual<int>(pack1.Count, 8);
            Assert.AreEqual<int>(pack2.Count, 8);

            Assert.AreNotEqual<List<Card>>(pack1, pack2);

        }

        [TestMethod]
        public void TestDistribute4()
        {
            CardPacket cards = new CardPacket();
            List<Card> pack1;
            List<Card> pack2;

            for (int i = 0; i < 100; i++)
            {
                cards.mix();
            }

            pack1 = cards.Distribute(8);
            pack2 = cards.Distribute(8);

            Assert.AreEqual<int>(pack1.Count, 8);
            Assert.AreEqual<int>(pack2.Count, 8);

            Assert.AreNotEqual<List<Card>>(pack1, pack2);

        }
    }
}
