using System;
using System.Collections.Generic;
using System.Linq;

namespace splodeyCats {
    class Program {
        public static int currentPlayer;
        public static List<Card> cards;
        public static List<Player> players;
        public static int playersLeft = 0;
        public static int numPlayers = 0;

        static void Main(string[] args) {
            
            #region player count
            int comboCardCount = 0;
            int attackCount = 0;
            
            
            // init player by getting number and setting
            while (numPlayers < 2 || numPlayers > 10) {
                Console.WriteLine("How many players do you want? Please enter a whole number from 2-10:");
                try { // catch non-ints
                    numPlayers = int.Parse(Console.ReadLine());
                }
                catch (FormatException) {
                    Console.WriteLine("Do you know what a whole number is?");
                    continue;
                }
                if(numPlayers >= 2 && numPlayers <= 3) {
                    comboCardCount = 3;
                    attackCount = 4;
                }
                else if (numPlayers >= 4 && numPlayers <= 7) {
                    comboCardCount = 4;
                    attackCount = 7;
                }
                else if (numPlayers >= 8 && numPlayers <= 10) {
                    comboCardCount = 7;
                    attackCount = 11;
                }
                else {
                    Console.WriteLine("Hey, that's not a valid player amount.");
                }
            }
            #endregion

            // make array list of players
            players = new List<Player>(numPlayers);


            #region combo cards
            // init cards
            cards = new List<Card>();
            for (int i = 0; i < comboCardCount; i++) {
                cards.Add(new ComboCard("Burrito Cat", ComboType.Burrito));
            }
            for (int i = 0; i < comboCardCount; i++) {
                cards.Add(new ComboCard("Cantelope Cat", ComboType.Cantelope));
            }
            for (int i = 0; i < comboCardCount; i++) {
                cards.Add(new ComboCard("Yams Cat", ComboType.Yams));
            }
            for (int i = 0; i < comboCardCount; i++) {
                cards.Add(new ComboCard("Mustache Cat", ComboType.Mustache));
            }
            for (int i = 0; i < comboCardCount; i++) {
                cards.Add(new ComboCard("RGB Cat", ComboType.RGBCat));
            }
            #endregion
            #region action cards
            for (int i = 0; i < attackCount; i++) {
                cards.Add(new Attack("Attack"));
            }
            #endregion
            cards = Shuffle(cards);

            // loop to set
            for (int i = 0; i < numPlayers; i++) {
                players.Add(new Player(i + 1));
                for (int j = 0; j < 4; j++) {
                    players[i].hand.Add(cards[0]);
                    cards.RemoveAt(0);
                }
                players[i].hand.Add(new Defuse("Defuse"));
            }
            // insert splodey cats
            for (int i = 0; i < numPlayers-1; i++) {
                cards.Add(new SplodeyCat("'splodey Cat"));
            }
            cards = Shuffle(cards);

            currentPlayer = 0;
            /*// print player's hands
            for (int i = 0; i < players.Count; i++) {
                Console.WriteLine(players[i].Name + ":");
                for (int j = 0; j < players[i].hand.Count; j++) {
                    Console.WriteLine("\t" + players[i].hand[j].name);
                }
            }
            // print deck
            Console.WriteLine("Deck:");
            for (int i = 0; i < cards.Count; i++) {
                Console.WriteLine("\t" + cards[i].name);
            }*/

            // main loop
            playersLeft = players.Count;
            while(playersLeft > 1) {
                //Console.Clear();
                int locCur = currentPlayer;
                // if dead, skip
                if (players[locCur].Dead) {
                    currentPlayer = (currentPlayer + 1) % numPlayers;
                    continue;
                }
                players[locCur].TurnsLeft++;
                while (players[locCur].TurnsLeft > 0) {
                    Console.WriteLine(players[locCur].Name);
                    Console.WriteLine("Turns left: " + players[locCur].TurnsLeft);
                    Console.WriteLine("Hand:");
                    foreach (Card c in players[locCur].hand) {
                        Console.WriteLine("\t" + c.name);
                    }
                    while (true) {
                        Console.WriteLine("Actions:");
                        int actionIndex = 1;
                        for (int i = 0; i < players[locCur].hand.Count; i++) {
                            if (players[locCur].hand[i] is ActionCard) {
                                ((ActionCard)players[locCur].hand[i]).choiceNum = actionIndex;
                                Console.WriteLine("\t" + actionIndex + ": " + players[locCur].hand[i].name);
                                actionIndex++;
                            }
                        }
                        Console.WriteLine("\t" + actionIndex + ": Draw (ends turns)");
                        int input = 0;
                        do {
                            try {
                                input = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException) {
                                input = 0;
                            }
                            if (input < 1 || input > actionIndex) {
                                input = 0;
                                Console.WriteLine("Not valid action choice. Try again.");
                            }
                            
                        }
                        while (input == 0);
                        if (input == actionIndex) {
                            Draw(locCur);
                            players[locCur].TurnsLeft--;
                            break;
                        }
                        ActionCard action = new ActionCard("blank");
                        foreach(Card c in players[locCur].hand) {
                            if (c is ActionCard) {
                                if(((ActionCard)c).choiceNum == input) {
                                    action = (ActionCard)c;
                                }
                            }
                        }
                        players[locCur].hand.Remove(action);
                        action.Action();
                    }
                    players[locCur].TurnsLeft--;
                }

                currentPlayer = (currentPlayer + 1) % numPlayers;

            }
        }


        // shuffle method
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
        // draw a card
        public static void Draw(int playerIndex) {
            Card drawnCard = cards[0];
            Console.WriteLine("You drew " + drawnCard.name);
            cards.RemoveAt(0);
            if(drawnCard is SplodeyCat) {
                if(players[playerIndex].hand.OfType<Defuse>().Any()) {
                    string input = "";
                    do {
                        Console.WriteLine("Use a defuse? (y/n)");
                        input = Console.ReadLine();
                        if (input.Trim().ToLower() == "y") {
                            bool removed = false;
                            for(int i = 0; i < players[playerIndex].hand.Count; i++) {
                                if(players[playerIndex].hand[i] is Defuse) {
                                    players[playerIndex].hand.RemoveAt(i);
                                    removed = true;
                                    break;
                                }
                            }
                            if (removed) {
                                bool flag = false;
                                int placeIn = 0;
                                while (!flag) {
                                    Console.WriteLine("Choose a location to put the exploding kitten between 0 and " + cards.Count);
                                    try {
                                        placeIn = int.Parse(Console.ReadLine());
                                    }
                                    catch(FormatException) {
                                        Console.WriteLine("No, that is not an option.");
                                    }
                                    if(placeIn >= 0 && placeIn <= cards.Count) {
                                        flag = true;
                                    }
                                }
                                cards.Insert(placeIn, drawnCard);
                            }
                            else {
                                Console.WriteLine("That did not work.");
                            }
                            
                            return;
                        }
                        else if (input.Trim().ToLower() == "n") {
                            Console.WriteLine("You die!");
                            players[playerIndex].Dead = true;
                            playersLeft--;
                            return;
                        }
                        Console.WriteLine("Invalid Input");
                    }
                    while (input.Trim().ToLower() != "y" || input.Trim().ToLower() != "n");
                }
                else {
                    Console.WriteLine("You die!");
                    players[playerIndex].Dead = true;
                    playersLeft--;
                    return;
                }
            }
            players[playerIndex].hand.Add(drawnCard);
        }
    }
}
