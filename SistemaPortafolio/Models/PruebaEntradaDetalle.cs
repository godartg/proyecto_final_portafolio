using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("PruebaEntradaDetalle")]
    public class PruebaEntradaDetalle
    {
        [Key]
        public int pruebaentradadetalle_id { get; set; }

        [Required]
        public int pruebaentrada_id { get; set; }

        [StringLength(250)]
        public string conocimiento { get; set; }

        public int deficiente { get; set; }

        public int suficiente { get; set; }

        public int bueno { get; set; }

        [ForeignKey("pruebaentrada_id")]
        public virtual PruebaEntrada PruebaEntrada { get; set; }
    }
}