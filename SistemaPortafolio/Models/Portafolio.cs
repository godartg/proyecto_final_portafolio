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
        
        public int matriculados { get; set; }

        public int retirados { get; set; }

        public int abandono { get; set; }

        public int asisten { get; set; }

        public int aprobados { get; set; }

        public int desaprobados { get; set; }

        public string material_digital { get; set; }

        public string material_impreso { get; set; }

        public string material_cantidad { get; set; }

        public string recepcionado_por { get; set; }

        [ForeignKey("cursodocente_id")]
        public virtual CursoDocente CursoDocente { get; set; }
    }
}