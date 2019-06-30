namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CursoDocente")]
    public partial class CursoDocente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CursoDocente()
        {
            Silabo = new HashSet<Silabo>();
        }

        [Key]
        public int cursodocente_id { get; set; }

        [Required]
        public int curso_id { get; set; }

        public int persona_id { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Persona Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Silabo> Silabo { get; set; }
    }
}
