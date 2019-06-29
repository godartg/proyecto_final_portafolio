namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoDocumento")]
    public partial class TipoDocumento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoDocumento()
        {
            Documento = new HashSet<Documento>();
            MetadataDocumento = new HashSet<MetadataDocumento>();
        }

        [Key]
        public int tipodocumento_id { get; set; }

        public int tipopersona_id { get; set; }

        [Required]
        [StringLength(100)]
        public string extension { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documento> Documento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetadataDocumento> MetadataDocumento { get; set; }

        public virtual TipoPersona TipoPersona { get; set; }
    }
}
