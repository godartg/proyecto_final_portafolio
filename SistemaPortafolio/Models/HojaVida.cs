namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HojaVida")]
    public partial class HojaVida
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HojaVida()
        {
            HojaVidaDocenteFA = new HashSet<HojaVidaDocenteFA>();
            HojaVidaDocenteFC = new HashSet<HojaVidaDocenteFC>();
            HojaVidaDocenteCRP = new HashSet<HojaVidaDocenteCRP>();
            HojaVidaDocenteEX = new HashSet<HojaVidaDocenteEX>();
        }

        [Key]
        public int hojavida_id { get; set; }

        public int persona_id { get; set; }

        public virtual Persona Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HojaVidaDocenteFA> HojaVidaDocenteFA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HojaVidaDocenteFC> HojaVidaDocenteFC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HojaVidaDocenteCRP> HojaVidaDocenteCRP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HojaVidaDocenteEX> HojaVidaDocenteEX { get; set; }
    }
}
