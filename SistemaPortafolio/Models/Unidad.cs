namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Unidad")]
    public partial class Unidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unidad()
        {
            ConfigEntrega = new HashSet<ConfigEntrega>();
            Documento = new HashSet<Documento>();
            MetadataDocumento = new HashSet<MetadataDocumento>();
        }

        [Key]
        public int id_unidad { get; set; }

        public int id_semestre { get; set; }

        [StringLength(150)]
        public string descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfigEntrega> ConfigEntrega { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documento> Documento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetadataDocumento> MetadataDocumento { get; set; }

        public virtual Semestre Semestre { get; set; }
    }
}
