using SistemaPortafolio.Filters;
using SistemaPortafolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaPortafolio.Areas.User.Controllers
{
    [Autenticado]
    public class HojaVidaDocentePublicacionesController : Controller
    {
        private HojaVida hojavida = new HojaVida();
        private HojaVidaDocentePublicaciones crp = new HojaVidaDocentePublicaciones();
        Usuario usuario = new Usuario();
        // GET: Usuario
        public ActionResult Index()
        {
            usuario.Obtener(SessionHelper.GetUser());
            return View(crp.Listar(usuario.Persona.persona_id));

        }
        //grilla
        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(crp.ListarGrilla(grid), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Ver(int id)
        {
            return View(crp.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            usuario.Obtener(SessionHelper.GetUser());
            ViewBag.Persona = crp.Listar(usuario.Persona.persona_id);    //para el combo
            ViewBag.Rango = hojavida.Listar();    //para el combo
            return View(
                id == 0 ? new HojaVidaDocentePublicaciones()//generar un nuevo semestre
                : crp.Obtener(id)//devuelve un registro por el id
                );
        }
        public ActionResult Guardar(Usuario model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/HojaVidaDocentePublicaciones");//se referencia al index automaticamente
            }
            else
            {
                return View("~/views/HojaVidaDocentePublicaciones/AgregarEditar.cshtml", model);
            }
        }
        public ActionResult Eliminar(int id)
        {
            crp.idHojaVidaDocentePublicaciones = id;
            crp.Eliminar();
            return Redirect("~/Admin/HojaVida/AgregarEditarP");
        }
    }
}