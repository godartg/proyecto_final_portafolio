namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        public int usuario_id { get; set; }

        public int persona_id { get; set; }

        public int tipousuario { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string clave { get; set; }

        [Required]
        [StringLength(200)]
        public string avatar { get; set; }

        [Required]
        [StringLength(10)]
        public string estado { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual TipoUsuario TipoUsuario1 { get; set; }
    }
}
