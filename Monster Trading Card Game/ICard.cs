using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public interface ICard 
    {
        string CardName { get; }
        string CardType { get; }
        int CardId { get; }
        int CardDamage { get; }
        bool Activity { get; }
        void ChangeActivity();
        Element NextElement();


    }
}