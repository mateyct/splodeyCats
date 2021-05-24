using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splodeyCats {
    class Attack : ActionCard {
        public Attack(string name) : base(name) {
            
        }

        public override void Action() {
            Program.players[Program.currentPlayer].TurnsLeft = 0;
            Program.players[(Program.currentPlayer + 1) % Program.numPlayers].TurnsLeft++;
        }
    }
}
