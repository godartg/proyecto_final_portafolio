namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlanEstudio")]
    public partial class PlanEstudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanEstudio()
        {
            Curso = new HashSet<Curso>();
        }

        [Key]
        public int plan_id { get; set; }

        public int? semestre_id { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curso> Curso { get; set; }

        public virtual Semestre Semestre { get; set; }
    }
}
