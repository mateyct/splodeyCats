using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splodeyCats {
    class ActionCard : Card {
        public int choiceNum { get; set; }
        public ActionCard(string name) : base(name) {
            
        } 
        
        public virtual void Action() {
            
        }
    }
}
