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
    public class InformeFinalsController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        int idUsuario = SessionHelper.GetUser();

        // GET: User/InformeFinals
        public ActionResult Index()
        {
            var personaId = db.Usuario.Find(idUsuario).persona_id;
            var informeFinal = db.InformeFinal.Include(i => i.CursoDocente).Where(x => x.CursoDocente.Persona.persona_id == personaId);
            return View(informeFinal.ToList());
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

        // GET: User/InformeFinals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformeFinal informeFinal = db.InformeFinal.Find(id);
            if (informeFinal == null)
            {
                return HttpNotFound();
            }
            return View(informeFinal);
        }

        public async Task<ActionResult> Pdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var informeFinal = db.InformeFinal.Find(id);
            if (informeFinal == null)
            {
                return HttpNotFound();
            }

            
            //RUTA GRAFICOS
            var documento = new Documento();
            var personaId = db.Usuario.Find(idUsuario).persona_id;
            var cursos = db.CursoDocente.Where(x => x.persona_id == personaId).Select(x => x.Curso).ToList();

            var curso = db.Curso.Find(informeFinal.CursoDocente.curso_id);
            var planEstudio = db.PlanEstudio.FirstOrDefault(x => x.estado == "Activo");
            var docente = db.Persona.Find(personaId);

            var cursoNombre = curso.curso_cod + " " + curso.curso_id;
            var planEstudioNombre = planEstudio.nombre;
            var docenteNombre = docente.nombre + " " + docente.apellido;

            var rutaServer = "~/Server/EPIS/Docs/InformeFinal/";
            var rutaOneDrive = "EPIS/Portafolio/Portafolio" + planEstudioNombre + "/" + docenteNombre + "/" + cursoNombre + "/5.Informe_Final/";

            var path = Path.Combine(Server.MapPath(rutaServer), "InformeFinal" + id + ".pdf");
            var report = new Rotativa.ActionAsPdf("Details", new { id });
            var pdfBytes = report.BuildFile(ControllerContext);
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
            fileStream.Close();

            //var result = await OfficeAccessSession.UploadFileAsync(Path.Combine(Server.MapPath(rutaServer), fileName), rutaOneDrive + fileName);
            string result = await OfficeAccessSession.UploadFileAsync(path, rutaOneDrive + "InformeFinal_" + id + ".pdf");

            return report;
        }

        // GET: User/InformeFinals/Create
        public ActionResult Create()
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");
            return View();
        }

        // POST: User/InformeFinals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InformeFinal informeFinal, string[] capacidad, string[] nivel, string[] aula)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                informeFinal.observacion_aulavirtual = aula.Aggregate("", (current, a) => current + (a + "@@@"));
                db.InformeFinal.Add(informeFinal);
                db.SaveChanges();

                for (var i = 0; i < capacidad.Length; i++)
                {
                    var nuevoDetalle = new InformeFinalDetalle()
                    {
                        informefinal_id = informeFinal.informefinal_id,
                        capacidad = capacidad[i],
                        nivel_alcanzado = nivel[i]
                    };
                    db.InformeFinalDetalle.Add(nuevoDetalle);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", informeFinal.cursodocente_id);
            return View(informeFinal);
        }

        // GET: User/InformeFinals/Edit/5
        public ActionResult Edit(int? id)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformeFinal informeFinal = db.InformeFinal.Find(id);
            if (informeFinal == null)
            {
                return HttpNotFound();
            }
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", informeFinal.cursodocente_id);
            return View(informeFinal);
        }

        // POST: User/InformeFinals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InformeFinal informeFinal, string[] capacidad, string[] nivel, string[] aula)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                informeFinal.observacion_aulavirtual = aula.Aggregate("", (current, a) => current + (a + "@@@"));
                db.Entry(informeFinal).State = EntityState.Modified;
                db.SaveChanges();

                var detalleList = db.InformeFinalDetalle.Where(x => x.informefinal_id == informeFinal.informefinal_id).ToList();
                db.InformeFinalDetalle.RemoveRange(detalleList);
                db.SaveChanges();

                for (var i = 0; i < capacidad.Length; i++)
                {
                    var nuevoDetalle = new InformeFinalDetalle()
                    {
                        informefinal_id = informeFinal.informefinal_id,
                        capacidad = capacidad[i],
                        nivel_alcanzado = nivel[i]
                    };
                    db.InformeFinalDetalle.Add(nuevoDetalle);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", informeFinal.cursodocente_id);
            return View(informeFinal);
        }

        // GET: User/InformeFinals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformeFinal informeFinal = db.InformeFinal.Find(id);
            if (informeFinal == null)
            {
                return HttpNotFound();
            }
            return View(informeFinal);
        }

        // POST: User/InformeFinals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var informeFinal = db.InformeFinal.Find(id);
            var finalDetalles = db.InformeFinalDetalle.Where(x => x.informefinal_id == id).ToList();
            foreach (var detalle in finalDetalles)
            {
                db.InformeFinalDetalle.Remove(detalle);
            }
            db.InformeFinal.Remove(informeFinal);
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
