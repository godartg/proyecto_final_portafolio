namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HojaVidaDocenteFA")]
    public partial class HojaVidaDocenteFA
    {
        [Key]
        public int hojavidadocentefa_id { get; set; }

        public int hojavida_id { get; set; }

        [Required]
        [StringLength(150)]
        public string institucion { get; set; }

        [Required]
        [StringLength(150)]
        public string titulo { get; set; }

        public virtual HojaVida HojaVida { get; set; }
    }
}
