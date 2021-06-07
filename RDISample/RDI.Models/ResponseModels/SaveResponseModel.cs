using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Models.ResponseModels
{
    public class SaveResponseModel
    {
        public DateTime CreationDate { get; set; }
        public Guid CardId { get; set; }
        public string TokenId { get; set; }
    }
}
