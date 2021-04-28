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

        public Player (int number) {
            Name = "Player " + number;
            Name = ChooseName();
            Sploded = false;
            hand = new List<Card>();
        }

        private string ChooseName() {
            Console.WriteLine(Name + ": What do you want your name to be?");
            return Console.ReadLine();
        }
    }
}
