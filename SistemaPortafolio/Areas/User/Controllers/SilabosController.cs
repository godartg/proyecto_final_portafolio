using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;

namespace SistemaPortafolio.Areas.User.Controllers
{
    public class SilabosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();

        // GET: User/Silabos
        public ActionResult Index()
        {
            var silabo = db.Silabo.Include(s => s.CursoDocente);
            return View(silabo.ToList());
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
            return View(silabo);
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
