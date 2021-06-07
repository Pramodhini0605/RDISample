using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RDI.Models.RequestModels
{
    public class ValidateRequestModel
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public Guid CardId { get; set; }
        [Required]
        public Guid TokenId { get; set; }
        [Required]
        [MaxLength(5)]
        public int CVV { get; set; }
    }
}
