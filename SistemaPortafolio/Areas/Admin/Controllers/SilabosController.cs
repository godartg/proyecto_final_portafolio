using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;
using Wired.RazorPdf;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SistemaPortafolio.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SistemaPortafolio.CSOneDriveAccess;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    public class SilabosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        Documento documento = new Documento();
        Usuario usuario = new Usuario().Obtener(SessionHelper.GetUser());
        // GET: Admin/Silabos
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

        // GET: Admin/Silabos/Details/5
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
            //documento.persona_id = usuario.Persona.persona_id;
            //documento.tipodocumento_id = 1;
            //documento.descripcion = "Curriculum Vitae ICACIT";
            //documento.estado = "activo";
            //documento.GuardarArchivoDirecto(pdf, usuario.Persona.persona_id, "HojaDeVida.pdf", "Curriculum Vitae ICACIT");
            
            //return new FileContentResult(pdf, "application/pdf");
            return View(silabo);

        }

        public async Task<ActionResult> Pdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var silabo = db.Silabo.Find(id);

            if (silabo == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(Server.MapPath("~/Server"), "Silabo" + id + ".pdf");
            var report = new Rotativa.ActionAsPdf("Details", new { id });

            var pdfBytes = report.BuildFile(ControllerContext);
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
            fileStream.Close();


            string result = await OfficeAccessSession.UploadFileAsync(path, "Server/Docs/Silabo" + id + ".pdf");

            return report;
        }

        // GET: Admin/Silabos/Create
        public ActionResult Create()
        {
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente, "cursodocente_id", "cursodocente_id");
            return View();
        }

        // POST: Admin/Silabos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "silabo_id,cursodocente_id,descripcion,bibliografia,competencias_curso,temas,resultados")] Silabo silabo)
        {
            if (ModelState.IsValid)
            {
                db.Silabo.Add(silabo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente, "cursodocente_id", "cursodocente_id", silabo.cursodocente_id);
            return View(silabo);
        }

        // GET: Admin/Silabos/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente, "cursodocente_id", "cursodocente_id", silabo.cursodocente_id);
            return View(silabo);
        }

        // POST: Admin/Silabos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "silabo_id,cursodocente_id,descripcion,bibliografia,competencias_curso,temas,resultados")] Silabo silabo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(silabo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente, "cursodocente_id", "cursodocente_id", silabo.cursodocente_id);
            return View(silabo);
        }

        // GET: Admin/Silabos/Delete/5
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

        // POST: Admin/Silabos/Delete/5
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
