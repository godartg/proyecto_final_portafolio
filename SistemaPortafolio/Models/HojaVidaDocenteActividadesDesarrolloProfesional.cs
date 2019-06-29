using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("HojaVidaDocenteActividadesDesarrolloProfesional")]
    public class HojaVidaDocenteActividadesDesarrolloProfesional
    {
        [Key]
        public int idHojaVidaDocenteActividadesDesarrolloProfesional { get; set; }
        public int hojavida_id { get; set; }
        public string institucion { get; set; }
        public string actividad { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime fechainicio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime fechafin { get; set; }
        public virtual HojaVida HojaVida { get; set; }
        public List<HojaVidaDocenteActividadesDesarrolloProfesional> Listar(int codigo)
        {
            var persona = new List<HojaVidaDocenteActividadesDesarrolloProfesional>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteActividadesDesarrolloProfesionals.Where(x => x.HojaVida.persona_id == codigo).ToList();
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
                    var query = db.HojaVidaDocenteActividadesDesarrolloProfesionals.Where(x => x.idHojaVidaDocenteActividadesDesarrolloProfesional > 0);
                    //obtener los campos y que permita ordenar
                    if (grilla.columna == "idHojaVidaDocenteActividadesDesarrolloProfesional")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.idHojaVidaDocenteActividadesDesarrolloProfesional)
                                                    : query.OrderBy(x => x.idHojaVidaDocenteActividadesDesarrolloProfesional);
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
                    if (grilla.columna == "actividad")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.actividad)
                                                    : query.OrderBy(x => x.actividad);
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
                        
                    }

                    var cargo = query.Skip(grilla.pagina)
                                    .Take(grilla.limite)
                                    .ToList();
                    var total = query.Count();//cantidad de registros

                    grilla.SetData(
                            from m in cargo
                            select new
                            {
                                m.idHojaVidaDocenteActividadesDesarrolloProfesional,
                                m.hojavida_id,
                                m.institucion,
                                m.actividad,
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
        public HojaVidaDocenteActividadesDesarrolloProfesional Obtener(int id)//retornar un objeto
        {
            var persona = new HojaVidaDocenteActividadesDesarrolloProfesional();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteActividadesDesarrolloProfesionals
                        .Where(x => x.idHojaVidaDocenteActividadesDesarrolloProfesional  == id)
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
        public List<HojaVidaDocenteActividadesDesarrolloProfesional> Buscar(string criterio)//retornar un objeto
        {
            var persona = new List<HojaVidaDocenteActividadesDesarrolloProfesional>();
            //   String estado = "";
            //    if (criterio == "Activo") estado = "Activo";
            //   if (criterio == "Inactivo") estado = "Inactivo";
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteActividadesDesarrolloProfesionals
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
                    if (this.idHojaVidaDocenteActividadesDesarrolloProfesional > 0)
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