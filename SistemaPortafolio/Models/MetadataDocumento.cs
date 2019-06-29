namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MetadataDocumento")]
    public partial class MetadataDocumento
    {
        [Key]
        public int metadata_id { get; set; }

        public int? documento_id { get; set; }

        [Required]
        [StringLength(100)]
        public string cod_curso { get; set; }

        public int persona_id { get; set; }

        public int semestre_id { get; set; }

        public int tipodocumento_id { get; set; }

        public int? id_unidad { get; set; }

        public int? pagina_total { get; set; }

        public int? palabra_total { get; set; }

        public int? caracter_total { get; set; }

        public int? linea_total { get; set; }

        public int? parrafo_total { get; set; }

        public int? celda { get; set; }

        public int? columna { get; set; }

        public int? diapositiva { get; set; }

        [StringLength(100)]
        public string tamanio { get; set; }

        [StringLength(100)]
        public string programa_nombre { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_creacion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_subida { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Documento Documento { get; set; }

        public virtual Unidad Unidad { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Semestre Semestre { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}
