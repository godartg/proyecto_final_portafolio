using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("HojaVidaDocentePublicaciones")]

    public class HojaVidaDocentePublicaciones
    {
        [Key]
        public int idHojaVidaDocentePublicaciones { get; set; }
        public int hojavida_id { get; set; }
        [StringLength(150)]
        public string tipoProduccion { get; set; }
        [StringLength(150)]
        public string titulo { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string primerAutor { get; set; }
        [Column(TypeName = "text")]
        public string coAutores { get; set; }
        [StringLength(50)]
        public string ano { get; set; }
        [Column(TypeName = "text")]
        public string doi { get; set; }
        [Column(TypeName = "text")]
        public string fuente { get; set; }
        public virtual HojaVida HojaVida { get; set; }
        public List<HojaVidaDocentePublicaciones> Listar(int codigo)
        {
            var persona = new List<HojaVidaDocentePublicaciones>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocentePublicaciones.Where(x => x.HojaVida.persona_id == codigo).ToList();
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
                    var query = db.HojaVidaDocentePublicaciones.Where(x => x.idHojaVidaDocentePublicaciones > 0);
                    //obtener los campos y que permita ordenar
                    if (grilla.columna == "idHojaVidaDocentePublicaciones")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.idHojaVidaDocentePublicaciones)
                                                    : query.OrderBy(x => x.idHojaVidaDocentePublicaciones);
                    }
                    if (grilla.columna == "hojavida_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.hojavida_id)
                                                    : query.OrderBy(x => x.hojavida_id);
                    }
                    if (grilla.columna == "tipoProduccion")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.tipoProduccion)
                                                    : query.OrderBy(x => x.tipoProduccion);
                    }
                    if (grilla.columna == "titulo")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.titulo)
                                                    : query.OrderBy(x => x.titulo);
                    }
                    if (grilla.columna == "primerAutor")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.primerAutor)
                                                    : query.OrderBy(x => x.primerAutor);
                    }
                    if (grilla.columna == "coAutores")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.coAutores)
                                                    : query.OrderBy(x => x.coAutores);
                    }
                    if (grilla.columna == "ano")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.ano)
                                                    : query.OrderBy(x => x.ano);
                    }
                    if (grilla.columna == "doi")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.doi)
                                                    : query.OrderBy(x => x.doi);
                    }
                    if (grilla.columna == "fuente")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fuente)
                                                    : query.OrderBy(x => x.fuente);
                    }
                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "titulo")
                            query = query.Where(x => x.titulo.StartsWith(f.valor));
                        if (f.columna == "primerAutor")
                            query = query.Where(x => x.primerAutor.StartsWith(f.valor));
                    }

                    var cargo = query.Skip(grilla.pagina)
                                    .Take(grilla.limite)
                                    .ToList();
                    var total = query.Count();//cantidad de registros

                    grilla.SetData(
                            from m in cargo
                            select new
                            {
                                m.idHojaVidaDocentePublicaciones,
                                m.hojavida_id,
                                m.tipoProduccion,
                                m.titulo,
                                m.primerAutor,
                                m.coAutores,
                                m.ano,
                                m.doi,
                                m.fuente,
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
        public HojaVidaDocentePublicaciones Obtener(int id)//retornar un objeto
        {
            var persona = new HojaVidaDocentePublicaciones();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocentePublicaciones
                        .Where(x => x.idHojaVidaDocentePublicaciones == id)
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
        public List<HojaVidaDocentePublicaciones> Buscar(string criterio)//retornar un objeto
        {
            var persona = new List<HojaVidaDocentePublicaciones>();
            //   String estado = "";
            //    if (criterio == "Activo") estado = "Activo";
            //   if (criterio == "Inactivo") estado = "Inactivo";
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocentePublicaciones
                            .Where(x => x.titulo.Contains(criterio) || x.ano == ano)
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
                    if (this.idHojaVidaDocentePublicaciones > 0)
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
    }
}