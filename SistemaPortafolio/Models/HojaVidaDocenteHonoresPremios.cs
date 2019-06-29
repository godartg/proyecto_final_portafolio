using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("HojaVidaDocenteHonoresPremios")]
    public class HojaVidaDocenteHonoresPremios
    {
        [Key]
        public int idHojaVidaDocenteHonoresPremios { get; set; }
        public int hojavida_id { get; set; }
        [Required]
        [StringLength(150)]
        public string institucion { get; set; }
        [Required]
        [StringLength(150)]
        public string titulo { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime fecha { get; set; }
        public virtual HojaVida HojaVida { get; set; }
        public List<HojaVidaDocenteHonoresPremios> Listar(int codigo)
        {
            var persona = new List<HojaVidaDocenteHonoresPremios>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteHonoresPremios.Where(x => x.HojaVida.persona_id == codigo).ToList();
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
                    var query = db.HojaVidaDocenteHonoresPremios.Where(x => x.idHojaVidaDocenteHonoresPremios > 0);
                    //obtener los campos y que permita ordenar
                    if (grilla.columna == "idHojaVidaDocenteHonoresPremios")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.idHojaVidaDocenteHonoresPremios)
                                                    : query.OrderBy(x => x.idHojaVidaDocenteHonoresPremios);
                    }
                    if (grilla.columna == "hojavida_id")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.hojavida_id)
                                                    : query.OrderBy(x => x.hojavida_id);
                    }
                    if (grilla.columna == "institucion")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.institucion)
                                                    : query.OrderBy(x => x.institucion);
                    }
                    if (grilla.columna == "titulo")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.titulo)
                                                    : query.OrderBy(x => x.titulo);
                    }
                    if (grilla.columna == "fecha")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fecha)
                                                    : query.OrderBy(x => x.fecha);
                    }
                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "institucion")
                            query = query.Where(x => x.institucion.StartsWith(f.valor));
                        if (f.columna == "titulo")
                            query = query.Where(x => x.titulo.StartsWith(f.valor));
                    }

                    var cargo = query.Skip(grilla.pagina)
                                    .Take(grilla.limite)
                                    .ToList();
                    var total = query.Count();//cantidad de registros

                    grilla.SetData(
                            from m in cargo
                            select new
                            {
                                m.idHojaVidaDocenteHonoresPremios,
                                m.hojavida_id,
                                m.institucion,
                                m.titulo,
                                m.fecha,
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
        public HojaVidaDocenteHonoresPremios Obtener(int id)//retornar un objeto
        {
            var persona = new HojaVidaDocenteHonoresPremios();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteHonoresPremios
                        .Where(x => x.idHojaVidaDocenteHonoresPremios == id)
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
        public List<HojaVidaDocenteHonoresPremios> Buscar(string criterio)//retornar un objeto
        {
            var persona = new List<HojaVidaDocenteHonoresPremios>();
            //   String estado = "";
            //    if (criterio == "Activo") estado = "Activo";
            //   if (criterio == "Inactivo") estado = "Inactivo";
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteHonoresPremios
                            .Where(x => x.institucion.Contains(criterio) || x.fecha == fecha)
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
                    if (this.idHojaVidaDocenteHonoresPremios > 0)
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