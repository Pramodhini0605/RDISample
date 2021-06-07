using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Entities
{
    public class Card
    {
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public int CustomerId { get; set; }
        public Guid CardId { get; set; }
        public string TokenId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
