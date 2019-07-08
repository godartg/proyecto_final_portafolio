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
using Wired.RazorPdf;

namespace SistemaPortafolio.Areas.User.Controllers
{
    public class SilabosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        int idUsuario = SessionHelper.GetUser();

        // GET: User/Silabos
        public ActionResult Index()
        {
            var silabo = db.Silabo.Include(s => s.CursoDocente);
            return View(silabo.ToList());
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

        // GET: User/Silabos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Silabo silabo = db.Silabo.Find(id);
            if (silabo == null)
            {
                return HttpNotFound();
            }

            //var generator = new MvcGenerator(ControllerContext);
            //var pdf = generator.GeneratePdf(silabo, "Details");
            //return new FileContentResult(pdf, "application/pdf");
            return View(silabo);
        }

        public async Task<ActionResult> Pdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Silabo silabo = db.Silabo.Find(id);
            if (silabo == null)
            {
                return HttpNotFound();
            }

            //RUTA GRAFICOS
            var documento = new Documento();
            var personaId = silabo.CursoDocente.Persona.persona_id;
            var cursos = db.CursoDocente.Where(x => x.persona_id == personaId).Select(x => x.Curso).ToList();

            var curso = db.Curso.Find(silabo.CursoDocente.curso_id);
            var planEstudio = db.PlanEstudio.FirstOrDefault(x => x.estado == "Activo");
            var docente = db.Persona.Find(personaId);

            var cursoNombre = curso.curso_cod + " " + curso.nombre;
            var planEstudioNombre = planEstudio.nombre;
            var docenteNombre = docente.nombre + " " + docente.apellido;

            var rutaServer = "~/Server/EPIS/Docs/Silabo/";
            var rutaOneDrive = "EPIS/Portafolio/Portafolio" + planEstudioNombre + "/" + docenteNombre + "/" + cursoNombre + "/2.Silabos_UPT_ICACIT/";
            Directory.CreateDirectory(Server.MapPath(rutaServer));

            var path = Path.Combine(Server.MapPath(rutaServer), "Silabo_" + id + ".pdf");
            var report = new Rotativa.ActionAsPdf("Details", new { id });
            var pdfBytes = report.BuildFile(ControllerContext);
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
            fileStream.Close();

            //var result = await OfficeAccessSession.UploadFileAsync(Path.Combine(Server.MapPath(rutaServer), fileName), rutaOneDrive + fileName);
            string result = await OfficeAccessSession.UploadFileAsync(path, rutaOneDrive + "Silabo_" + id + ".pdf");

            return report;
        }

        // GET: User/Silabos/Create
        public ActionResult Create()
        {
            var idUsuario = SessionHelper.GetUser();
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select( x => x.persona_id).FirstOrDefault();

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");
            return View();
        }

        // POST: User/Silabos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Silabo silabo, string[] aportes)
        {
            var idUsuario = SessionHelper.GetUser();
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                foreach (var aporte in aportes)
                {
                    silabo.resultados += aporte + "@@@";
                }
                db.Silabo.Add(silabo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                    .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", silabo.cursodocente_id);

            return View(silabo);
        }

        // GET: User/Silabos/Edit/5
        public ActionResult Edit(int? id)
        {
            var idUsuario = SessionHelper.GetUser();
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Silabo silabo = db.Silabo.Find(id);

            if (silabo != null){
                var resultadosList = silabo.resultados.Split(new[] { "@@@" }, StringSplitOptions.None);
                ViewBag.ResultadosList = resultadosList;
            }

            if (silabo == null)
            {
                return HttpNotFound();
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", silabo.cursodocente_id);

            return View(silabo);
        }

        // POST: User/Silabos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Silabo silabo, string[] aportes)
        {
            var idUsuario = SessionHelper.GetUser();
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                silabo.resultados = "";
                foreach (var aporte in aportes)
                {
                    silabo.resultados += aporte + "@@@";
                }
                db.Entry(silabo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", silabo.cursodocente_id);

            return View(silabo);
        }

        // GET: User/Silabos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Silabo silabo = db.Silabo.Find(id);
            if (silabo == null)
            {
                return HttpNotFound();
            }
            return View(silabo);
        }

        // POST: User/Silabos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Silabo silabo = db.Silabo.Find(id);
            db.Silabo.Remove(silabo);
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
