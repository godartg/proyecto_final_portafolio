namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HojaVidaDocenteEX")]
    public partial class HojaVidaDocenteEX
    {
        [Key]
        public int hojavidadocenteex_id { get; set; }

        public int hojavida_id { get; set; }

        [Required]
        [StringLength(150)]
        public string institucion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fechainicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fechafin { get; set; }

        [Column(TypeName = "text")]
        public string funcion { get; set; }

        public virtual HojaVida HojaVida { get; set; }
    }
}
