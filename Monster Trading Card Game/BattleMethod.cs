using System;

namespace Monster_Trading_Card_Game
{
    public class BattleMethod
    {
        public int MXM(float damage1, float damage2 , int id1 , int id2)
        {
            if(damage1 > damage2)
            {
                return id1;
            }
            else if(damage2 > damage1)
            {
                return id2;
            }
            else
            {
                return -1;
            }
        }
        public int SXS(float damage1, string element1 ,  float damage2 ,  string element2, int id1, int id2)
        {
            float x; float  y;
            if (element1 == "fire")
            {
                x = damage1 / 2;
            }else if(element1 == "water") { x = damage1 * 2; }
            else { x = damage1; }
            if (element2 == "fire")
            {
                y = damage2 / 2;
            }
            else if (element2 == "water") { y = damage2 * 2; }
            else { y = damage2; }

            if (x > y)
            {
                return id1;
            }
            else if (y > x)
            {
                return id2;
            }
            else
            {
                return -1;
            }
        }

        public int MXS(float damage1, string element1, float damage2, string element2, int id1, int id2)
        {
            float x; float y;
            if (element1 == "fire")
            {
                x = damage1 / 2;
            }
            else if (element1 == "water") { x = damage1 * 2; }
            else { x = damage1; }
            if (element2 == "fire")
            {
                y = damage2 / 2;
            }
            else if (element2 == "water") { y = damage2 * 2; }
            else { y = damage2; }

            if (x > y)
            {
                return id1;
            }
            else if (y > x)
            {
                return id2;
            }
            else
            {
                return -1;
            }
        }
    }
}