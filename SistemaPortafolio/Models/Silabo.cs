using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("CursoDocente")]
    public class Silabo
    {
        [Key]
        public int silabo_id { get; set; }

        [Required]
        public int cursodocente_id { get; set; }

        [Column(TypeName = "text")]
        public string descripcion{ get; set; }

        [Column(TypeName = "text")]
        public string bibliografia { get; set; }

        [Column(TypeName = "text")]
        public string competencias_curso { get; set; }

        [Column(TypeName = "text")]
        public string temas { get; set; }

        [StringLength(150)]
        public string resultados { get; set; } 

        public virtual CursoDocente CursoDocente { get; set; }
    }
}