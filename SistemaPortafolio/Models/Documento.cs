namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Documento")]
    public partial class Documento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Documento()
        {
            MetadataDocumento = new HashSet<MetadataDocumento>();
        }

        [Key]
        public int documento_id { get; set; }

        public int tipodocumento_id { get; set; }

        public int persona_id { get; set; }

        public int? id_unidad { get; set; }

        [Required]
        [StringLength(100)]
        public string curso_cod { get; set; }

        [Required]
        [StringLength(150)]
        public string archivo { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string descripcion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fecha_entrega { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime hora_entrega { get; set; }

        [StringLength(10)]
        public string extension { get; set; }

        [StringLength(50)]
        public string tamanio { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Unidad Unidad { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetadataDocumento> MetadataDocumento { get; set; }
    }
}
