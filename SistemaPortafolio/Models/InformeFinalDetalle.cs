using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("InformeFinalDetalle")]
    public class InformeFinalDetalle
    {
        [Key]
        public int informefinaldetalle_id { get; set; }

        [Required]
        public int informefinal_id { get; set; }

        [Column(TypeName = "text")]
        public string capacidad { get; set; }

        public string nivel_alcanzado { get; set; }

        [ForeignKey("informefinal_id")]
        public virtual InformeFinal InformeFinal { get; set; }
    }
}