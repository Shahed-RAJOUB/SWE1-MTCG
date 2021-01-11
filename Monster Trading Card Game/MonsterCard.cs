using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class MonsterCard : IGenerator
    {
        private static Random random = new Random();
        private static Random randName = new Random();
        private static Random randElem = new Random();
        
        
        public string RandomStringGenerator()
        {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string (Enumerable.Repeat(chars, 6)
                 .Select(s => s[random.Next(6)]).ToArray());
        }

        public string Name()
        {
            string[] Monsters = { "Dragon", "Bear", "Lion", "Knight",
          "Goblen", "Eagle", "Zombi", "Clone",
              "Fish", "Chicken" };
            int index = randName.Next(Monsters.Length);
            return Monsters[index];

        }
        public string element()
        {
            string[] Monsters = { "fire", "water", "normal" };
            int index = randElem.Next(Monsters.Length);
            return Monsters[index];

        }

        public Card RandumCard()
        {
             Random randDamage = new Random();
            return  new Card {id = RandomStringGenerator(), Name = Name(), element = element(), damage = randDamage.Next(200), deckId = 0, type = "Monster"  };
        }

     
    }
}
