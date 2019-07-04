﻿using System;
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
    public class InformeFinalsController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        int idUsuario = SessionHelper.GetUser();

        // GET: User/InformeFinals
        public ActionResult Index()
        {
            var personaId = db.Usuario.Find(idUsuario).persona_id;
            var informeFinal = db.InformeFinal.Include(i => i.CursoDocente);
            return View(informeFinal.ToList());
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

        public ActionResult Pdf(int? id)
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

            var report = new Rotativa.ActionAsPdf("Details", new { id });
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
            InformeFinal informeFinal = db.InformeFinal.Find(id);
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
