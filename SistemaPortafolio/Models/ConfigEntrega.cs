namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfigEntrega")]
    public partial class ConfigEntrega
    {
        [Key]
        public int configentrega_id { get; set; }

        public int id_unidad { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fecha_inicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fecha_fin { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        public virtual Unidad Unidad { get; set; }
    }
}
