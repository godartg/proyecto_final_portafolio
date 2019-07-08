using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SistemaPortafolio.Models;
using System.IO;
using SistemaPortafolio.CSOneDriveAccess;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    public class PruebaEntradasController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        int idUsuario = SessionHelper.GetUser();
        Usuario usuario = new Usuario().Obtener(SessionHelper.GetUser());
        // GET: Admin/PruebaEntradas
        public ActionResult Index()
        {
            var pruebaEntrada = db.PruebaEntrada.Include(p => p.CursoDocente);
            return View(pruebaEntrada.ToList());
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

        // GET: Admin/PruebaEntradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaEntrada pruebaEntrada = db.PruebaEntrada.Find(id);
            if (pruebaEntrada == null)
            {
                return HttpNotFound();
            }
            return View(pruebaEntrada);
        }

        public async Task<ActionResult> Pdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaEntrada pruebaEntrada = db.PruebaEntrada.Find(id);
            if (pruebaEntrada == null)
            {
                return HttpNotFound();
            }

            //RUTA GRAFICOS
            var documento = new Documento();
            var personaId = pruebaEntrada.CursoDocente.Persona.persona_id;
            var cursos = db.CursoDocente.Where(x => x.persona_id == personaId).Select(x => x.Curso).ToList();

            var curso = db.Curso.Find(pruebaEntrada.CursoDocente.curso_id);
            var planEstudio = db.PlanEstudio.FirstOrDefault(x => x.estado == "Activo");
            var docente = db.Persona.Find(personaId);

            var cursoNombre = curso.curso_cod + " " + curso.nombre;
            var planEstudioNombre = planEstudio.nombre;
            var docenteNombre = docente.nombre + " " + docente.apellido;

            var rutaServer = "~/Server/EPIS/Docs/PruebaEntrada/";
            var rutaOneDrive = "EPIS/Portafolio/Portafolio" + planEstudioNombre + "/" + docenteNombre + "/" + cursoNombre + "/3.Prueba_de_Entrada/";
            Directory.CreateDirectory(Server.MapPath(rutaServer));

            var path = Path.Combine(Server.MapPath(rutaServer), "PruebaEntrada" + id + ".pdf");
            var report = new Rotativa.ActionAsPdf("Details", new { id });
            var pdfBytes = report.BuildFile(ControllerContext);
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
            fileStream.Close();

            //var result = await OfficeAccessSession.UploadFileAsync(Path.Combine(Server.MapPath(rutaServer), fileName), rutaOneDrive + fileName);
            string result = await OfficeAccessSession.UploadFileAsync(path, rutaOneDrive + "PruebaEntrada_" + id + ".pdf");

            return report;
        }

        // GET: Admin/PruebaEntradas/Create
        public ActionResult Create()
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");

            return View();
        }

        // POST: Admin/PruebaEntradas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PruebaEntrada pruebaEntrada, PruebaEntradaDetalle[] pruebaEntradaDetalles, string[] medidas)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            var medidasCadena = "";
            foreach (var medida in medidas)
            {
                medidasCadena += medida + "@@@";
            }

            if (ModelState.IsValid)
            {
                pruebaEntrada.medidas_correctivas = medidasCadena;
                db.PruebaEntrada.Add(pruebaEntrada);
                db.SaveChanges();

                foreach (var prueba in pruebaEntradaDetalles)
                {
                    prueba.pruebaentrada_id = pruebaEntrada.pruebaentrada_id;
                    db.PruebaEntradaDetalle.Add(prueba);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", pruebaEntrada.cursodocente_id);

            return View(pruebaEntrada);
        }

        // GET: Admin/PruebaEntradas/Edit/5
        public ActionResult Edit(int? id)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaEntrada pruebaEntrada = db.PruebaEntrada.Find(id);
            if (pruebaEntrada == null)
            {
                return HttpNotFound();
            }
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", pruebaEntrada.cursodocente_id);

            return View(pruebaEntrada);
        }

        // POST: Admin/PruebaEntradas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PruebaEntrada pruebaEntrada, PruebaEntradaDetalle[] pruebaEntradaDetalles, string[] medidas)
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            var medidasCadena = "";
            foreach (var medida in medidas)
            {
                medidasCadena += medida + "@@@";
            }

            if (ModelState.IsValid)
            {
                pruebaEntrada.medidas_correctivas = medidasCadena;
                db.Entry(pruebaEntrada).State = EntityState.Modified;
                db.SaveChanges();

                foreach (var pruebaEntradaDetalle in pruebaEntradaDetalles)
                {
                    pruebaEntradaDetalle.pruebaentrada_id = pruebaEntrada.pruebaentrada_id;

                    if (pruebaEntradaDetalle.pruebaentradadetalle_id != 0)
                    {
                        var pruebaEntradaObject = db.PruebaEntradaDetalle.Find(pruebaEntradaDetalle.pruebaentradadetalle_id);
                        db.PruebaEntradaDetalle.Remove(pruebaEntradaObject ?? throw new InvalidOperationException());
                        db.SaveChanges();
                    }
                    db.PruebaEntradaDetalle.Add(pruebaEntradaDetalle);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre", pruebaEntrada.cursodocente_id);

            return View(pruebaEntrada);
        }

        // GET: Admin/PruebaEntradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaEntrada pruebaEntrada = db.PruebaEntrada.Find(id);
            if (pruebaEntrada == null)
            {
                return HttpNotFound();
            }
            return View(pruebaEntrada);
        }

        // POST: Admin/PruebaEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PruebaEntrada pruebaEntrada = db.PruebaEntrada.Find(id);
            foreach (var pruebaEntradaDetalle in pruebaEntrada.PruebaEntradaDetalle)
            {
                db.PruebaEntradaDetalle.Remove(pruebaEntradaDetalle);
                db.SaveChanges();
            }
            db.PruebaEntrada.Remove(pruebaEntrada);
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
