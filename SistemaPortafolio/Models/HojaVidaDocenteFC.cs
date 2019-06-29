namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HojaVidaDocenteFC")]
    public partial class HojaVidaDocenteFC
    {
        [Key]
        public int hojavidadocentefc_id { get; set; }

        public int hojavida_id { get; set; }

        [Required]
        [StringLength(150)]
        public string institucion { get; set; }

        [Required]
        [StringLength(150)]
        public string tiempo_duracion { get; set; }

        [Required]
        [StringLength(150)]
        public string duracion { get; set; }

        [Required]
        [StringLength(150)]
        public string curso { get; set; }

        public virtual HojaVida HojaVida { get; set; }
    }
}
