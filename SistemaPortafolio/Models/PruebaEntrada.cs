using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("PruebaEntrada")]
    public class PruebaEntrada
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PruebaEntrada()
        {
            PruebaEntradaDetalle = new HashSet<PruebaEntradaDetalle>();
        }

        [Key]
        public int pruebaentrada_id { get; set; }

        [Required]
        public int cursodocente_id { get; set; }

        public int matriculados { get; set; }

        public int evaluados { get; set; }

        [StringLength(150)]
        public string medidas_correctivas { get; set; }

        [Column(TypeName = "text")]
        public string recomendaciones { get; set; }

        [ForeignKey("cursodocente_id")]
        public virtual CursoDocente CursoDocente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PruebaEntradaDetalle> PruebaEntradaDetalle { get; set; }
    }
}