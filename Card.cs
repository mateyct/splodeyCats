using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splodeyCats {
    /// <summary>
    /// Base card class for all of the cards
    /// </summary>
    class Card {

        public string name { get; set; }
        
        public Card(string name) {
            this.name = name;
            // cool
        }
    }
}
