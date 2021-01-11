using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monster_Trading_Card_Game
{
   public class SpellCard
    {
        private static Random random = new Random();
        private static Random randName = new Random();
        private static Random randElem = new Random();
        

        public string RandomStringGenerator()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
             .Select(s => s[random.Next(6)]).ToArray());
        }

        public string Name()
        {
            string[] Spells = { "Witch", "Wizard", "Genie", "DarkKnight",
              "mouse", "Fairy", "Bird", "",
              "queen", "Beans" };
            int index = randName.Next(Spells.Length);
            return Spells[index];

        }
        public string element()
        {
            string[] Elements = { "fire", "water", "normal" };
            int index = randElem.Next(Elements.Length);
            return Elements[index];

        }

        public Card RandumCard()
        {
            Random randDamage = new Random();
            return new Card { id = RandomStringGenerator(), Name = Name(), element = element(), damage = randDamage.Next(200), deckId = 0, type = "Spell" };
        }

    }
}
