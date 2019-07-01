using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;
using SistemaPortafolio.Filters;
using Wired.Razor;
using Wired.RazorPdf;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    [Autenticado]
    public class HojaVidaController : Controller
    {
        HojaVida hojavida = new HojaVida();
        HojaVidaDocenteFA hojavidadocentefa = new HojaVidaDocenteFA();
        HojaVidaDocenteFC hojavidadocentefc = new HojaVidaDocenteFC();
        HojaVidaDocenteEX hojavidadocenteex = new HojaVidaDocenteEX();
        HojaVidaDocenteCRP hojavidadocentecrp = new HojaVidaDocenteCRP();
        HojaVidaDocenteActividadesDesarrolloProfesional hojavidadocenteadp = new HojaVidaDocenteActividadesDesarrolloProfesional();
        HojaVidaDocenteActividadServicios hojavidadocenteas = new HojaVidaDocenteActividadServicios();
        HojaVidaDocenteHonoresPremios hojavidadocentehp = new HojaVidaDocenteHonoresPremios();
        HojaVidaDocenteMembresia hojavidadocentem = new HojaVidaDocenteMembresia();
        HojaVidaDocentePublicaciones hojavidadocentep = new HojaVidaDocentePublicaciones();
        Usuario usuario = new Usuario().Obtener(SessionHelper.GetUser());
        Documento documento = new Documento();
        Parser parser = new Parser();

        // GET: Admin/HojaVida
        public ActionResult Index()
        {
            return View(hojavida.Listar());
        }

        public ActionResult MiHojaVida()
        {
            int hojavida_id = ObtenerHojaVidaId(0);

            List<HojaVida> hojas = new List<HojaVida>();

            hojas.Add(hojavida.Obtener(hojavida_id));

            return View(hojas);
        }

        public ActionResult Imprimir()
        {
            /*
            if (usuario_id > 0)
            {
                usuario = new Usuario().Obtener(usuario_id);
            }
            */
            Documento doc = new Documento();
            TipoDocumento tipoDocumento = new TipoDocumento();
            int hojavida_id = ObtenerHojaVidaId(0);

            HojaVida hoja = new HojaVida();
            
            hoja = hojavida.Obtener(hojavida_id);

            ViewData["hojavidadocentefa"] = hojavidadocentefa.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentefc"] = hojavidadocentefc.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocenteex"] = hojavidadocenteex.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentecrp"] = hojavidadocentecrp.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocenteadp"] = hojavidadocenteadp.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocenteas"] = hojavidadocenteas.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentehp"] = hojavidadocentehp.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentem"] = hojavidadocentem.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentep"] = hojavidadocentep.Listar(usuario.Persona.persona_id);
            var generator = new MvcGenerator(ControllerContext);
            var pdf = generator.GeneratePdf(hoja, "Imprimir");
            documento.persona_id = usuario.Persona.persona_id;
            documento.tipodocumento_id = 1;
            documento.descripcion = "Curriculum Vitae ICACIT";
            documento.estado = "activo";
            documento.GuardarArchivoDirecto(pdf, usuario.Persona.persona_id, "HojaDeVida.pdf", "Curriculum Vitae ICACIT");
            return new FileContentResult(pdf, "application/pdf");

        }

        public int ObtenerHojaVidaId(int id = 0)
        {
            if (id <= 0)
            {

                hojavida = hojavida.ObtenerByPersona(usuario.persona_id);

                if (hojavida == null)
                {
                    hojavida = new HojaVida();
                    hojavida.persona_id = usuario.persona_id;
                    hojavida.Guardar();

                    id = hojavida.hojavida_id;
                }
                else
                {
                    id = hojavida.hojavida_id;
                }
            }

            return id;
        }

        [HttpGet]
        public ActionResult AgregarEditarFA(int id = 0)
        {
            if (id > 0)
            {
                hojavidadocentefa = hojavidadocentefa.Obtener(id);
            }
            else
            {
                hojavidadocentefa.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentefa.Listar(usuario.Persona.persona_id);


            return View(hojavidadocentefa);
        }

        [HttpPost]
        public ActionResult AgregarEditarFA(HojaVidaDocenteFA model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarFA");
        }

        //AgregarEditarFC

        [HttpGet]
        public ActionResult AgregarEditarFC(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentefc = hojavidadocentefc.Obtener(id);
            }
            else
            {
                hojavidadocentefc.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentefc.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentefc);
        }

        [HttpPost]
        public ActionResult AgregarEditarFC(HojaVidaDocenteFC model)
        {

            model.hojavida_id = 2;

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarFC");
        }

        //AgregarEditarEX
        [HttpGet]
        public ActionResult AgregarEditarEX(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocenteex = hojavidadocenteex.Obtener(id);
            }
            else
            {
                hojavidadocenteex.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocenteex.Listar(usuario.Persona.persona_id);

            return View(hojavidadocenteex);
        }

        [HttpPost]
        public ActionResult AgregarEditarEX(HojaVidaDocenteEX model)
        {

            model.fechainicio = (DateTime)model.fechainicio;
            model.fechafin = (DateTime)model.fechafin;
            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarEX");
        }

        //AgregarEditarCRP
        [HttpGet]
        public ActionResult AgregarEditarCRP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentecrp = hojavidadocentecrp.Obtener(id);
            }
            else
            {
                hojavidadocentecrp.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentecrp.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentecrp);
        }

        [HttpPost]
        public ActionResult AgregarEditarCRP(HojaVidaDocenteCRP model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarCRP");
        }
        //AgregarEditarP
        [HttpGet]
        public ActionResult AgregarEditarP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentep = hojavidadocentep.Obtener(id);
            }
            else
            {
                hojavidadocentep.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentep.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentep);
        }

        [HttpPost]
        public ActionResult AgregarEditarP(HojaVidaDocentePublicaciones model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarP");
        }
        //AgregarEditarADP
        [HttpGet]
        public ActionResult AgregarEditarADP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocenteadp = hojavidadocenteadp.Obtener(id);
            }
            else
            {
                hojavidadocenteadp.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocenteadp.Listar(usuario.Persona.persona_id);

            return View(hojavidadocenteadp);
        }

        [HttpPost]
        public ActionResult AgregarEditarADP(HojaVidaDocenteActividadesDesarrolloProfesional model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarADP");
        }
        //AgregarEditarAS
        [HttpGet]
        public ActionResult AgregarEditarAS(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocenteas = hojavidadocenteas.Obtener(id);
            }
            else
            {
                hojavidadocenteas.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocenteas.Listar(usuario.Persona.persona_id);

            return View(hojavidadocenteas);
        }

        [HttpPost]
        public ActionResult AgregarEditarAS(HojaVidaDocenteActividadServicios model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarAS");
        }
        //AgregarEditarHP
        [HttpGet]
        public ActionResult AgregarEditarHP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentehp = hojavidadocentehp.Obtener(id);
            }
            else
            {
                hojavidadocentehp.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentehp.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentehp);
        }

        [HttpPost]
        public ActionResult AgregarEditarHP(HojaVidaDocenteHonoresPremios model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarHP");
        }
        //AgregarEditarM
        [HttpGet]
        public ActionResult AgregarEditarM(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentem = hojavidadocentem.Obtener(id);
            }
            else
            {
                hojavidadocentem.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentem.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentem);
        }

        [HttpPost]
        public ActionResult AgregarEditarM(HojaVidaDocenteMembresia model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarM");
        }
    }
}