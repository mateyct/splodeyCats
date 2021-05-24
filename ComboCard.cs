using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace splodeyCats {
    class ComboCard : Card {
        public ComboType type;

        public ComboCard(string name, ComboType _type) : base(name) {
            type = _type;
        }
    }
    enum ComboType {
        Burrito,
        Cantelope,
        Yams,
        Mustache,
        RGBCat
    }
}
