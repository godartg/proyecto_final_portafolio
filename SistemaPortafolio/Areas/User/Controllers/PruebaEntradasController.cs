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
    public class PruebaEntradasController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        int idUsuario = SessionHelper.GetUser();

        // GET: User/PruebaEntradas
        public ActionResult Index()
        {
            var personaId = db.Usuario.Find(idUsuario).persona_id;
            var pruebaEntrada = db.PruebaEntrada.Include(p => p.CursoDocente).Where(x => x.CursoDocente.Persona.persona_id == personaId);
            return View(pruebaEntrada.ToList());
        }

        // GET: User/PruebaEntradas/Details/5
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

        public ActionResult Pdf(int? id)
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

            var report = new Rotativa.ActionAsPdf("Details", new {id});
            return report;
        }

        // GET: User/PruebaEntradas/Create
        public ActionResult Create()
        {
            var personaId = db.Usuario
                .Where(x => x.usuario_id == idUsuario)
                .Select(x => x.persona_id).FirstOrDefault();

            ViewBag.cursodocente_id = new SelectList(db.CursoDocente
                .Where(x => x.persona_id == personaId), "cursodocente_id", "Curso.nombre");

            return View();
        }

        // POST: User/PruebaEntradas/Create
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

        // GET: User/PruebaEntradas/Edit/5
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

        // POST: User/PruebaEntradas/Edit/5
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

        // GET: User/PruebaEntradas/Delete/5
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

        // POST: User/PruebaEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PruebaEntrada pruebaEntrada = db.PruebaEntrada.Find(id);
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
