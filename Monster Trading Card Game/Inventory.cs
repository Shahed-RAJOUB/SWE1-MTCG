using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public interface Inventory
    {
        int Packages { get; set; }

        void PackagesTrading();
    }
}