using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public interface Battle_Arena
    {
        event EventHandler PlayersBattleMethod;

        void StartBattle();
        void WinnerIs();
    }
}