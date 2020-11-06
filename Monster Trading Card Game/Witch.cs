using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class Witch : SuperSpellCard
    {
        public Witch() :
            base("Witch", "Spell", 21, 10, true, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }
    }
    public class Wizard : SuperSpellCard
    {
        public Wizard() :
            base("Wizard", "Spell", 22, 15, true, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }
    }
    public class FireElve : SuperSpellCard
    {
        public FireElve() :
            base("FireElve", "Spell", 23, 5, true, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }
        
    }
    public class Ork : SuperSpellCard
    {
        public Ork() :
            base("Ork", "Spell", 24, 20, true, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }

    }
    public class WaterSpell : SuperSpellCard
    {
        public WaterSpell() :
            base("WaterSpell", "Spell", 25, 8 , true, new[] { Element.Fire, Element.Normal, Element.Water })
        {
        }

    }
}