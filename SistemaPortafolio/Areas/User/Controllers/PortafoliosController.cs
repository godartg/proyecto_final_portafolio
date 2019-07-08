using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.CSOneDriveAccess;
using SistemaPortafolio.Models;

namespace SistemaPortafolio.Areas.User.Controllers
{
    public class PortafoliosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        int _idUsuario = SessionHelper.GetUser();
        Usuario usuario = new Usuario().Obtener(SessionHelper.GetUser());
        List<string> unidadesList = new List<string>() { "I", "II", "III"};


        // GET: User/Portafolios
        public ActionResult Index()
        {
            var personaId = db.Usuario.Find(_idUsuario).persona_id;
            var portafolio = db.Portafolio.Include(p => p.CursoDocente).Where(x => x.CursoDocente.Persona.persona_id == personaId);
            return View(portafolio.ToList());
        }

        public O365RestSession OfficeAccessSession
        {
            get
            {
                var officeAccess = Session["OfficeAccess"];
                if (officeAccess == null)
                {
                    officeAccess = new O365RestSession(Token.ClientId, Token.Secret, Token.CallbackUri);
                    Session["OfficeAccess"] = officeAccess;
                }
                return officeAccess as O365RestSession;
            }
        }

        // GET: User/Portafolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portafolio portafolio = db.Portafolio.Find(id);
            if (portafolio == null)
            {
                return HttpNotFound();
            }
            return View(portafolio);
        }

        public async Task<ActionResult> Pdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var portafolio = db.Portafolio.Find(id);

            if (portafolio == null)
            {
                return HttpNotFound();
            }

            //RUTA GRAFICOS
            var documento = new Documento();
            var personaId = portafolio.CursoDocente.Persona.persona_id;
            var cursos = db.CursoDocente.Where(x => x.persona_id == personaId).Select(x => x.Curso).ToList();

            var curso = db.Curso.Find(portafolio.CursoDocente.curso_id);
            var planEstudio = db.PlanEstudio.FirstOrDefault(x => x.estado == "Activo");
            var docente = db.Persona.Find(personaId);

            var cursoNombre = curso.curso_cod + " " + curso.nombre;
            var planEstudioNombre = planEstudio.nombre;
            var docenteNombre = docente.nombre + " " + docente.apellido;

            var rutaServer = "~/Server/EPIS/Docs/Portafolio/";
            var rutaOneDrive = "EPIS/Portafolio/Portafolio" + planEstudioNombre + "/" + docenteNombre + "/" + cursoNombre + "/4.Portafolio_por_Unidad/" + portafolio.unidad + "_Unidad/";
            Directory.CreateDirectory(Server.MapPath(rutaServer));

            var path = Path.Combine(Server.MapPath(rutaServer), "Portafolio" + id + ".pdf");
            var report = new Rotativa.ActionAsPdf("Details", new { id });
            var pdfBytes = report.BuildFile(ControllerContext);
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
            fileStream.Close();

            string result = await OfficeAccessSession.UploadFileAsync(path, rutaOneDrive + "Portafolio" + id + ".pdf");

            return report;
        }

        // GET: User/Portafolios/Create
        public ActionResult Create()
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == _idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            ViewBag.unidad = new SelectList(unidadesList);
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");
            return View();
        }

        // POST: User/Portafolios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Portafolio portafolio, string[] materialDigital, string[] materialImpreso, string[] materialCantidad)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == _idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            var materialDigitalCadena = materialDigital.Aggregate("", (current, m) => current + (m + "@@@"));
            var materialImpresoCadena = materialImpreso.Aggregate("", (current, m) => current + (m + "@@@"));
            var materialCantidadCadena = materialCantidad.Aggregate("", (current, m) => current + (m + "@@@"));

            if (ModelState.IsValid)
            {
                portafolio.material_digital = materialDigitalCadena;
                portafolio.material_impreso = materialImpresoCadena;
                portafolio.material_cantidad = materialCantidadCadena;

                db.Portafolio.Add(portafolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.unidad = new SelectList(unidadesList);
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");
            return View(portafolio);
        }

        // GET: User/Portafolios/Edit/5
        public ActionResult Edit(int? id)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == _idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portafolio portafolio = db.Portafolio.Find(id);
            if (portafolio == null)
            {
                return HttpNotFound();
            }
            ViewBag.unidad = new SelectList(unidadesList);
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");
            return View(portafolio);
        }

        // POST: User/Portafolios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Portafolio portafolio, string[] materialDigital, string[] materialImpreso, string[] materialCantidad)
        {
            var materialDigitalCadena = materialDigital.Aggregate("", (current, m) => current + (m + "@@@"));
            var materialImpresoCadena = materialImpreso.Aggregate("", (current, m) => current + (m + "@@@"));
            var materialCantidadCadena = materialCantidad.Aggregate("", (current, m) => current + (m + "@@@"));

            if (ModelState.IsValid)
            {
                portafolio.material_digital = materialDigitalCadena;
                portafolio.material_impreso = materialImpresoCadena;
                portafolio.material_cantidad = materialCantidadCadena;

                db.Entry(portafolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.unidad = new SelectList(unidadesList);
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente, "cursodocente_id", "cursodocente_id", portafolio.cursodocente_id);
            return View(portafolio);
        }

        // GET: User/Portafolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portafolio portafolio = db.Portafolio.Find(id);
            if (portafolio == null)
            {
                return HttpNotFound();
            }
            return View(portafolio);
        }

        // POST: User/Portafolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portafolio portafolio = db.Portafolio.Find(id);
            db.Portafolio.Remove(portafolio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
