namespace SistemaPortafolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("TipoPersona")]
    public partial class TipoPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPersona()
        {
            Persona = new HashSet<Persona>();
            TipoDocumento = new HashSet<TipoDocumento>();
        }

        [Key]
        public int tipopersona_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Persona> Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoDocumento> TipoDocumento { get; set; }
        public List<TipoPersona> Listar()
        {
            var persona = new List<TipoPersona>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.TipoPersona.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return persona;
        }


        //para la anexgrid
        public AnexGRIDResponde ListarGrilla(AnexGRID grilla)
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    grilla.Inicializar();
                    var query = db.TipoPersona.Where(x => x.tipopersona_id > 0);
                    //obtener los campos y que permita ordenar
                    if (grilla.columna == "tipopersona_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipopersona_id)
                                                    : query.OrderBy(x => x.tipopersona_id);
                    }
                    if (grilla.columna == "nombre")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.nombre)
                                                    : query.OrderBy(x => x.nombre);
                    }
                    if (grilla.columna == "estado")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.estado)
                                                    : query.OrderBy(x => x.estado);
                    }

                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "nombre")
                            query = query.Where(x => x.nombre.StartsWith(f.valor));
                        if (f.columna == "estado")
                            query = query.Where(x => x.estado.StartsWith(f.valor));
                    }

                    var cargo = query.Skip(grilla.pagina)
                                    .Take(grilla.limite)
                                    .ToList();
                    var total = query.Count();//cantidad de registros

                    grilla.SetData(
                            from m in cargo
                            select new
                            {
                                m.tipopersona_id,
                                m.nombre,
                                m.estado,
                            },
                            total
                        );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return grilla.responde();
        }


        //metodo obtener
        public TipoPersona Obtener(int id)//retornar un objeto
        {
            var persona = new TipoPersona();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.TipoPersona.Include("persona")
                        .Where(x => x.tipopersona_id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return persona;
        }
        //metodo buscar
        public List<TipoPersona> Buscar(string criterio)//retornar un objeto
        {
            var persona = new List<TipoPersona>();
            String estado = "";
            if (criterio == "Activo") estado = "Activo";
            if (criterio == "Inactivo") estado = "Inactivo";
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.TipoPersona
                            .Where(x => x.nombre.Contains(criterio) || x.estado == estado)
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return persona;
        }
        //METODO GUARDAR
        public void Guardar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    if (this.tipopersona_id > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //metodo eliminar
        public void Eliminar()
        {
            try
            {
                using (var db = new ModeloDatos())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //consulta una Persona cuantos usuarios tiene 
        public string[,] Consulta()
        {
            string[,] criterios;
            try
            {
                using (var db = new ModeloDatos())
                {
                    var per = (from m in db.Persona
                               join c in db.TipoPersona on
                               m.tipopersona_id equals c.tipopersona_id into r_join
                               from c in r_join.DefaultIfEmpty()
                               group new { m, c } by new
                               {
                                   m.tipopersona_id,
                                   m.nombre
                               } into g
                               select new
                               {
                                   id_persona = (int?)g.Key.tipopersona_id,
                                   g.Key.nombre,
                                   Cantidad = g.Count(p => p.c.tipopersona_id != null)
                               }).ToList();
                    criterios = new string[per.Count(), 2];
                    int count = 0;
                    foreach (var m in per)
                    {
                        criterios[count, 0] = Convert.ToString(m.nombre);
                        criterios[count, 1] = Convert.ToString(m.Cantidad);
                        count++;
                    }
                }
                return criterios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
