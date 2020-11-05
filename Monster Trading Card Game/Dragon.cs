using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Monster_Trading_Card_Game
{
    class Dragon : SuperMonsterCard
    {
        public Dragon() : base("Dragon", "Monster", 11, 5, false, new[] { Element.Fire, Element.Normal, Element.Water })
        { 
        }
    }

    class Goblin: SuperMonsterCard
    {
        public Goblin() : base("Goblin", "Monster", 12, 2, false, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }
    }
    class Kraken : SuperMonsterCard
    {
        public Kraken() : base("Kraken", "Monster", 13, 4, false, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }
    }
    class Knight : SuperMonsterCard
    {
        public Knight() : base("Knight", "Monster", 14, 10, false, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }
    }
}
