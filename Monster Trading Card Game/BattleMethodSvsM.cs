using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public class BattleMethodSvsM : SpellCard, Battle_Arena
    {
        public event EventHandler PlayersBattleMethod;

        public int RestPoints
        {
            get => default;
            set
            {
            }
        }

        public void StartBattle()
        {
            throw new System.NotImplementedException();
        }

        public void WinnerIs()
        {
            throw new System.NotImplementedException();
        }
    }
}