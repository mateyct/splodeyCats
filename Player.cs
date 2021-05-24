using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splodeyCats {
    class Player {

        public string Name { get; } // name

        public bool Sploded { get; set; }

        public List<Card> hand {get; set;}

        public int Number { get; }

        public int TurnsLeft { get; set; }

        public bool Dead { get; set; }

        public Player (int number) {
            Name = "Player " + number;
            Name = ChooseName();
            Number = number;
            Sploded = false;
            hand = new List<Card>();
            TurnsLeft = 0;
        }

        private string ChooseName() {
            Console.WriteLine(Name + ": What do you want your name to be?");
            return Console.ReadLine();
        }
    }
}
