using RDI.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Repositories
{
    public  class CardContext
    {
        private  Dictionary<Guid, Card> _card = new Dictionary<Guid, Card>();
        public CardContext()
        {

        }
        

        public bool AddCard(Card card)
        {
            _card.Add(card.CardId, card);
            return true;
        }

        public bool FindCard(Guid cardId)
        {
            return _card.ContainsKey(cardId);
        }

        public Card GetCard(Guid cardId)
        {
            Card card = null;
            _card.TryGetValue(cardId, out card);
            return card;
        }
    }
}
