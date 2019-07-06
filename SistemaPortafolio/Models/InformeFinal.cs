using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("InformeFinal")]
    public class InformeFinal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InformeFinal()
        {
            InformeFinalDetalle = new HashSet<InformeFinalDetalle>();
        }

        [Key]
        public int informefinal_id { get; set; }

        [Required]
        public int cursodocente_id { get; set; }

        public int porcentaje_silabo { get; set; }

        public int practicas { get; set; }

        public int laboratorios { get; set; }

        public int proyectos { get; set; }

        public int matriculados { get; set; }

        public int retiro { get; set; }

        public int abandono { get; set; }

        public int asisten { get; set; }

        public int aprobados { get; set; }

        public int desaprobados { get; set; }

        public int nota_alta { get; set; }

        public int nota_promedio { get; set; }

        public int nota_baja { get; set; }

        [Column(TypeName = "text")]
        public string motivo { get; set; }

        [Column(TypeName = "text")]
        public string observacion_estudiantes { get; set; }

        [Column(TypeName = "text")]
        public string observacion_asistencia { get; set; }

        [Column(TypeName = "text")]
        public string observacion_silabo { get; set; }

        [Column(TypeName = "text")]
        public string observacion_aulavirtual { get; set; }

        [Column(TypeName = "text")]
        public string observacion_administrativas { get; set; }

        [Column(TypeName = "text")]
        public string observacion_competencias { get; set; }

        [Column(TypeName = "text")]
        public string observacion_mejora { get; set; }

        [Column(TypeName = "text")]
        public string observacion_docente { get; set; }

        [Column(TypeName = "text")]
        public string observacion_recomendaciones { get; set; }

        [ForeignKey("cursodocente_id")]
        public virtual CursoDocente CursoDocente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformeFinalDetalle> InformeFinalDetalle { get; set; }
    }
}