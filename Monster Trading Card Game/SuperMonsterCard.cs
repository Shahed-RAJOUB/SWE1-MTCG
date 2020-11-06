using System;
using System.Collections.Generic;
using System.Text;

namespace Monster_Trading_Card_Game
{
    public abstract class SuperMonsterCard : ICard
    {
        protected SuperMonsterCard(string cardName , string cardType , int cardId , int cardDamage , bool activity , Element[] effect)
        {
            this.CardName = cardName;
            this.CardDamage = cardDamage;
            this.CardId = cardId;
            this.CardType = cardType;
            this._elements = effect;
            this.Activity = activity;
            this._currentElementIndex = 0;
            

        }

        public string CardName {get; }

        public string CardType {get; }

        public int CardId {get; }
        
        public int CardDamage { get; }
        
        public bool Activity { get; private set; }
        
        private int _currentElementIndex;
        private readonly Element[] _elements;
        public void ChangeActivity()
        {
            Activity = false;
        }

        public Element NextElement()
        {
           return _elements[_currentElementIndex = (_currentElementIndex++) % _elements.Length];
        }

   



    }
}