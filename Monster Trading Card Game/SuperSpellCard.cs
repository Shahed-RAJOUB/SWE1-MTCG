using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public abstract class SuperSpellCard : Card
    {
        public void SpellCardPoints()
        {
            throw new System.NotImplementedException();
        }

        public void CardEffect()
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class CopyOfSuperSpellCard
    {
    }
}