using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    [Table("HojaVidaDocenteMembresia")]
    public class HojaVidaDocenteMembresia
    {
        [Key]
        public int idHojaVidaDocenteMembresia { get; set; }
        public int hojavida_id { get; set; }
        [Required]
        [StringLength(150)]
        public string institucion { get; set; }
        [Required]
        [StringLength(150)]
        public string funcion { get; set; }
        [Required]
        [StringLength(20)]
        public string ano { get; set; }
        public virtual HojaVida HojaVida { get; set; }
        public List<HojaVidaDocenteMembresia> Listar(int codigo)
        {
            var persona = new List<HojaVidaDocenteMembresia>();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteMembresia.Where(x => x.HojaVida.persona_id == codigo).ToList();
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
                    var query = db.HojaVidaDocenteMembresia.Where(x => x.idHojaVidaDocenteMembresia > 0);
                    //obtener los campos y que permita ordenar
                    if (grilla.columna == "idHojaVidaDocenteMembresia")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.idHojaVidaDocenteMembresia)
                                                    : query.OrderBy(x => x.idHojaVidaDocenteMembresia);
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
                    if (grilla.columna == "funcion")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.funcion)
                                                    : query.OrderBy(x => x.funcion);
                    }
                    if (grilla.columna == "ano")
                    {
                        query = grilla.columna_orden == "DESC" ? query.OrderByDescending(x => x.ano)
                                                    : query.OrderBy(x => x.ano);
                    }
                    
                    // Filtrar
                    foreach (var f in grilla.filtros)
                    {
                        if (f.columna == "institucion")
                            query = query.Where(x => x.institucion.StartsWith(f.valor));
                        if (f.columna == "funcion")
                            query = query.Where(x => x.funcion.StartsWith(f.valor));
                    }

                    var cargo = query.Skip(grilla.pagina)
                                    .Take(grilla.limite)
                                    .ToList();
                    var total = query.Count();//cantidad de registros

                    grilla.SetData(
                            from m in cargo
                            select new
                            {
                                m.idHojaVidaDocenteMembresia,
                                m.hojavida_id,
                                m.institucion,
                                m.funcion,
                                m.ano,
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
        public HojaVidaDocenteMembresia Obtener(int id)//retornar un objeto
        {
            var persona = new HojaVidaDocenteMembresia();
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteMembresia
                        .Where(x => x.idHojaVidaDocenteMembresia == id)
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
        public List<HojaVidaDocenteMembresia> Buscar(string criterio)//retornar un objeto
        {
            var persona = new List<HojaVidaDocenteMembresia>();
            //   String estado = "";
            //    if (criterio == "Activo") estado = "Activo";
            //   if (criterio == "Inactivo") estado = "Inactivo";
            try
            {
                using (var db = new ModeloDatos())
                {
                    persona = db.HojaVidaDocenteMembresia
                            .Where(x => x.institucion.Contains(criterio) || x.ano == ano)
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
                    if (this.idHojaVidaDocenteMembresia > 0)
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