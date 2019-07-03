using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("Portafolio")]
    public class Portafolio
    {
        [Key]
        public int portafolio_id { get; set; }

        [Required]
        public int cursodocente_id { get; set; }

        [StringLength(10)]
        public string unidad { get; set; }
    }
}