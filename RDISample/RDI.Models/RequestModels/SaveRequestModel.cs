using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RDI.Models.RequestModels
{
    public class SaveRequestModel 
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(16)]
        public long CardNumber { get; set; }
        [Required]
        [MaxLength(5)]
        public int CVV { get; set; }
    }
}
