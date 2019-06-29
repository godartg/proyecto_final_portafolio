namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HojaVidaDocenteCRP")]
    public partial class HojaVidaDocenteCRP
    {
        [Key]
        public int hojavidadocentecrp_id { get; set; }

        public int hojavida_id { get; set; }

        [Required]
        [StringLength(150)]
        public string certificacion { get; set; }

        [Required]
        [StringLength(150)]
        public string institucion { get; set; }

        [Required]
        [StringLength(20)]
        public string año { get; set; }

        public virtual HojaVida HojaVida { get; set; }
    }
}
