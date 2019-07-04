using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    public class PortafoliosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        readonly int _idUsuario = SessionHelper.GetUser();
        readonly List<string> unidadesList = new List<string>() { "I", "II", "III" };

        // GET: Admin/Portafolios
        public ActionResult Index()
        {
            var personaId = db.Usuario.Find(_idUsuario).Persona.persona_id;
            var portafolio = db.Portafolio.Include(p => p.CursoDocente);
            return View(portafolio.ToList());
        }

        // GET: Admin/Portafolios/Details/5
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

        public ActionResult Pdf(int? id)
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

            var report = new Rotativa.ActionAsPdf("Details", new { id });
            return report;
        }

        // GET: Admin/Portafolios/Create
        public ActionResult Create()
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == _idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            ViewBag.unidad = new SelectList(unidadesList);
            ViewBag.cursodocente_id = new SelectList(db.CursoDocente.Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");
            return View();
        }

        // POST: Admin/Portafolios/Create
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

        // GET: Admin/Portafolios/Edit/5
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

        // POST: Admin/Portafolios/Edit/5
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

        // GET: Admin/Portafolios/Delete/5
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

        // POST: Admin/Portafolios/Delete/5
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
