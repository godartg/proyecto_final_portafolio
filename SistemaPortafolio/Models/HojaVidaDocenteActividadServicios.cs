using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("HojaVidaDocenteActividadServicios")]
    public class HojaVidaDocenteActividadServicios
    {
        [Key]
        public int idHojaVidaDocenteActividadServicios { get; set; }
        public int hojavida_id { get; set; }
        public string institucion { get; set; }
        public string servicio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime fechainicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fechafin { get; set; }
        public virtual HojaVida HojaVida { get; set; }
        public List<HojaVidaDocenteActividadServicios> Listar(int codigo)
        {
            var persona = new List<HojaVidaDocenteActividadServicios>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteActividadServicios.Where(x => x.HojaVida.persona_id == codigo).ToList();
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
                    var query = db.HojaVidaDocenteActividadServicios.Where(x => x.idHojaVidaDocenteActividadServicios > 0);
                    //obtener los campos y que permita ordenar
                    if (grilla.columna == "idHojaVidaDocenteActividadServicios")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.idHojaVidaDocenteActividadServicios)
                                                    : query.OrderBy(x => x.idHojaVidaDocenteActividadServicios);
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
                    if (grilla.columna == "servicio")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.servicio)
                                                    : query.OrderBy(x => x.servicio);
                    }
                    if (grilla.columna == "fechainicio")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fechainicio)
                                                    : query.OrderBy(x => x.fechainicio);
                    }
                    if (grilla.columna == "fechafin")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.fechafin)
                                                    : query.OrderBy(x => x.fechafin);
                    }
                    
                    
                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "institucion")
                            query = query.Where(x => x.institucion.StartsWith(f.valor));
                        if (f.columna == "servicio")
                            query = query.Where(x => x.servicio.StartsWith(f.valor));
                    }

                    var cargo = query.Skip(grilla.pagina)
                                    .Take(grilla.limite)
                                    .ToList();
                    var total = query.Count();//cantidad de registros

                    grilla.SetData(
                            from m in cargo
                            select new
                            {
                                m.idHojaVidaDocenteActividadServicios,
                                m.hojavida_id,
                                m.institucion,
                                m.servicio,
                                m.fechainicio,
                                m.fechafin,
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
        public HojaVidaDocenteActividadServicios Obtener(int id)//retornar un objeto
        {
            var persona = new HojaVidaDocenteActividadServicios();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteActividadServicios
                        .Where(x => x.idHojaVidaDocenteActividadServicios == id)
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
        public List<HojaVidaDocenteActividadServicios> Buscar(string criterio)//retornar un objeto
        {
            var persona = new List<HojaVidaDocenteActividadServicios>();
            //   String estado = "";
            //    if (criterio == "Activo") estado = "Activo";
            //   if (criterio == "Inactivo") estado = "Inactivo";
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteActividadServicios
                            .Where(x => x.institucion.Contains(criterio) || x.fechainicio == fechainicio)
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
                    if (this.idHojaVidaDocenteActividadServicios > 0)
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