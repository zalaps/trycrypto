using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SerializationBlockChain.Models
{
    public class SNView
    {
        [Required(ErrorMessage = "Serial Number Required.")]
        public string SN { get; set; }
        public string Status { get; set; }
    }
}
