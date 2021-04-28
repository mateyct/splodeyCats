using System;
using System.Collections.Generic;

namespace splodeyCats {
    class Program {
        static void Main(string[] args) {

            // init cards
            List<Card> cards = new List<Card>();
            for (int i = 0; i < 4; i++) {
                cards.Add(new ComboCard("Tacocat"));
            }
            for (int i = 0; i < 4; i++) {
                cards.Add(new ComboCard("Watermelon Cat"));
            }
            for (int i = 0; i < 4; i++) {
                cards.Add(new ComboCard("Potato Cat"));
            }
            for (int i = 0; i < 4; i++) {
                cards.Add(new ComboCard("Beard Cat"));
            }
            for (int i = 0; i < 4; i++) {
                cards.Add(new ComboCard("Rainbow Ralphing Cat"));
            }
            cards = Shuffle(cards);

            // init player by getting number and setting
            Console.WriteLine("How many players do you want?");
            int numPlayers = int.Parse(Console.ReadLine());
            List<Player> players = new List<Player>(numPlayers);
            // loop to set
            for (int i = 0; i < numPlayers; i++) {
                players.Add(new Player(i + 1));
                for (int j = 0; j < 4; j++) {
                    players[i].hand.Add(cards[0]);
                    cards.RemoveAt(0);
                }
            }
            int currentPlayer = 0;
            for (int i = 0; i < players.Count; i++) {
                Console.WriteLine(players[i].Name + ":");
                for (int j = 0; j < players[i].hand.Count; j++) {
                    Console.WriteLine("\t" + players[i].hand[j].name);
                }
            }
            Console.WriteLine("Deck:");
            for (int i = 0; i < cards.Count; i++) {
                Console.WriteLine("\t" + cards[i].name);
            }
        }
        public static List<T> Shuffle<T>(List<T> list) {
            Random rand = new Random();
            for (int i = 0; i < 10; i++) {
                int n = list.Count;
                while (n >= 1) {
                    n--;
                    int k = rand.Next(0, list.Count);
                    T val = list[k];
                    list[k] = list[n];
                    list[n] = val;
                }
            }
            return list;
        }
    }
}
