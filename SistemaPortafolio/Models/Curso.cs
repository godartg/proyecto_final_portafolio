namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            CursoAlumno = new HashSet<CursoAlumno>();
            CursoDocente = new HashSet<CursoDocente>();
            Documento = new HashSet<Documento>();
            MetadataDocumento = new HashSet<MetadataDocumento>();
        }

        [Key]
        [StringLength(100)]
        public string curso_cod { get; set; }

        public int plan_id { get; set; }

        public int ciclo_id { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        public int credito { get; set; }

        public int horasteoria { get; set; }

        public int horaspractica { get; set; }

        public int totalhoras { get; set; }

        [StringLength(150)]
        public string prerequisito { get; set; }

        [Required]
        [StringLength(100)]
        public string estado { get; set; }

        public virtual Ciclo Ciclo { get; set; }

        public virtual PlanEstudio PlanEstudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CursoAlumno> CursoAlumno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CursoDocente> CursoDocente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documento> Documento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetadataDocumento> MetadataDocumento { get; set; }
    }
}
