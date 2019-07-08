namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CursoDocente")]
    public partial class CursoDocente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CursoDocente()
        {
            Silabo = new HashSet<Silabo>();
            PruebaEntrada = new HashSet<PruebaEntrada>();
            PruebaEntrada = new HashSet<PruebaEntrada>();
            Portafolio = new HashSet<Portafolio>();
        }

        [Key]
        public int cursodocente_id { get; set; }

        [Required]
        public int curso_id { get; set; }

        public int persona_id { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Persona Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Silabo> Silabo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PruebaEntrada> PruebaEntrada { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Portafolio> Portafolio { get; set; }
        public List<CursoDocente> listarCursoDocente()
        {
            var cursoDocente = new List<CursoDocente>();
            try
            {
                using( var db= new ModeloDatos())
                {
                    cursoDocente = db.CursoDocente.ToList();
                }
            }catch(Exception e)
            {
                throw;
            }
            return cursoDocente;
        }
        
    }
}
