namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notificacion")]
    public partial class Notificacion
    {
        [Key]
        public int notificacion_id { get; set; }

        public int persona_emisor { get; set; }

        public int persona_receptor { get; set; }

        [Required]
        [StringLength(150)]
        public string titulo { get; set; }

        [Required]
        [StringLength(150)]
        public string asunto { get; set; }

        [Required]
        [StringLength(150)]
        public string mensaje { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fecha_emision { get; set; }

        [Required]
        [StringLength(100)]
        public string estado { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Persona Persona1 { get; set; }
    }
}
