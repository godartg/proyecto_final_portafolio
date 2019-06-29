namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoPersona")]
    public partial class TipoPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPersona()
        {
            Persona = new HashSet<Persona>();
            TipoDocumento = new HashSet<TipoDocumento>();
        }

        [Key]
        public int tipopersona_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Persona> Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoDocumento> TipoDocumento { get; set; }
    }
}
