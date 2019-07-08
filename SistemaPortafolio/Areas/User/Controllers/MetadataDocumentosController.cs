using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Alphaleonis.Win32.Filesystem;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.CSOneDriveAccess;
using SistemaPortafolio.Models;

namespace SistemaPortafolio.Areas.User.Controllers
{
    public class MetadataDocumentosController : Controller
    {
        private ModeloDatos db = new ModeloDatos();
        readonly int _idUsuario = SessionHelper.GetUser();

        // GET: User/MetadataDocumentos
        public ActionResult Index(int id)
        {
            var cursoId = db.CursoDocente.Find(id).curso_id;
            var personaId = db.CursoDocente.Find(id).persona_id;
            var metadataDocumento = db.MetadataDocumento
                .Include(m => m.Curso)
                .Include(m => m.Documento)
                .Include(m => m.Persona)
                .Include(m => m.Semestre)
                .Include(m => m.TipoDocumento)
                .Include(m => m.Unidad)
                .Where(x => x.curso_id == cursoId && x.persona_id == personaId);
            return View(metadataDocumento.ToList());
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

        // GET: User/MetadataDocumentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetadataDocumento metadataDocumento = db.MetadataDocumento.Find(id);
            if (metadataDocumento == null)
            {
                return HttpNotFound();
            }
            return View(metadataDocumento);
        }

        // GET: User/MetadataDocumentos/Create
        public ActionResult Create(int tipoRecurso)
        {
            var personaId = db.Usuario.Find(_idUsuario).persona_id;
            var cursos = db.CursoDocente.Where(x => x.persona_id == personaId).Select(x => x.Curso).ToList();

            var tipoDocumento = new List<TipoDocumento>();
            if (tipoRecurso == 2)
                tipoDocumento = db.TipoDocumento.Where(x => x.tipodocumento_id >= 8 && x.tipodocumento_id <= 12).ToList();
            else
                tipoDocumento = db.TipoDocumento.Where(x => x.tipodocumento_id >= 17 && x.tipodocumento_id <= 20).ToList();

            ViewBag.curso_id = new SelectList(cursos, "curso_id", "nombre");
            //ViewBag.documento_id = new SelectList(db.Documento, "documento_id", "archivo");
            //ViewBag.persona_id = new SelectList(db.Persona, "persona_id", "dni");
            ViewBag.semestre_id = new SelectList(db.Semestre.Where(x => x.estado == "Activo"), "semestre_id", "nombre");
            ViewBag.tipodocumento_id = new SelectList(tipoDocumento, "tipodocumento_id", "nombre");
            ViewBag.id_unidad = new SelectList(db.Unidad, "id_unidad", "descripcion");
            return View();
        }

        // POST: User/MetadataDocumentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MetadataDocumento metadataDocumento, HttpPostedFileBase file)
        {
            var documento = new Documento();
            var personaId = db.Usuario.Find(_idUsuario).persona_id;
            var cursos = db.CursoDocente.Where(x => x.persona_id == personaId).Select(x => x.Curso).ToList();

            var curso = db.Curso.Find(metadataDocumento.curso_id);
            var unidad = db.Unidad.Find(metadataDocumento.id_unidad);
            var tipoDocumento = db.TipoDocumento.Find(metadataDocumento.tipodocumento_id);
            var planEstudio = db.Curso.Find(metadataDocumento.curso_id).PlanEstudio;
            var docente = db.Persona.Find(personaId);

            var cursoNombre = curso.curso_cod + " " + curso.nombre;
            var unidadDescripcion = unidad.descripcion;
            var tipoRecurso = tipoDocumento.tipopersona_id == 2 ? "Recursos_Estudiantes" : "Recursos_Docente";
            var recurso = tipoDocumento.nombre;
            var planEstudioNombre = planEstudio.nombre;
            var docenteNombre = docente.nombre + " " + docente.apellido;

            //var rutaServer = "~/EPIS/Portafolio/Portafolio" + planEstudioNombre + "/" + docenteNombre + "/" + cursoNombre + "/4.Portafolio_por_Unidad/" + unidadDescripcion + "_Unidad/" + tipoRecurso + "/" + recurso + "/";
            var rutaServer = "~/Server/EPIS/Docs/" + recurso + "/";
            var rutaOneDrive = "EPIS/Portafolio/Portafolio" + planEstudioNombre + "/" + docenteNombre + "/" + cursoNombre + "/4.Portafolio_por_Unidad/" + unidadDescripcion + "_Unidad/" + tipoRecurso + "/" + recurso + "/";
            var fileName = "";
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    Directory.CreateDirectory(Server.MapPath(rutaServer));
                    var path = Path.Combine(Server.MapPath(rutaServer), fileName);
                    file.SaveAs(path);

                    var fechaEntrega = DateTime.Now.Date;
                    var horaEntrega = DateTime.Now;

                    documento = new Documento()
                    {
                        tipodocumento_id = metadataDocumento.tipodocumento_id,
                        persona_id = personaId,
                        id_unidad = metadataDocumento.id_unidad,
                        curso_id = metadataDocumento.curso_id,
                        archivo = fileName,
                        descripcion = rutaServer,
                        fecha_entrega = fechaEntrega,
                        hora_entrega = horaEntrega,
                        estado = "Activo"
                    };
                    db.Documento.Add(documento);
                    db.SaveChanges();
                }

                metadataDocumento.documento_id = documento.documento_id;
                metadataDocumento.persona_id = personaId;
                db.MetadataDocumento.Add(metadataDocumento);
                db.SaveChanges();
                var result = await OfficeAccessSession.UploadFileAsync(Path.Combine(Server.MapPath(rutaServer), fileName), rutaOneDrive + fileName);

                var cursoDocenteId = db.CursoDocente.Where(x =>
                    x.curso_id == metadataDocumento.curso_id && x.persona_id == metadataDocumento.persona_id).FirstOrDefault().cursodocente_id;
                return RedirectToAction("Index", new{id= cursoDocenteId});
            }

            //var tipoDocumento = new List<TipoDocumento>();
            //if (tipoRecurso == 2)
            //    tipoDocumento = db.TipoDocumento.Where(x => x.tipodocumento_id >= 8 && x.tipodocumento_id <= 12).ToList();
            //else
            //    tipoDocumento = db.TipoDocumento.Where(x => x.tipodocumento_id >= 17 && x.tipodocumento_id <= 20).ToList();

            ViewBag.curso_id = new SelectList(cursos, "curso_id", "nombre");
            //ViewBag.documento_id = new SelectList(db.Documento, "documento_id", "archivo");
            //ViewBag.persona_id = new SelectList(db.Persona, "persona_id", "dni");
            ViewBag.semestre_id = new SelectList(db.Semestre.Where(x => x.estado == "Activo"), "semestre_id", "nombre");
            ViewBag.tipodocumento_id = new SelectList(db.TipoDocumento, "tipodocumento_id", "nombre");
            ViewBag.id_unidad = new SelectList(db.Unidad, "id_unidad", "descripcion");

            return View(metadataDocumento);
        }

        // GET: User/MetadataDocumentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetadataDocumento metadataDocumento = db.MetadataDocumento.Find(id);
            if (metadataDocumento == null)
            {
                return HttpNotFound();
            }
            ViewBag.curso_id = new SelectList(db.Curso, "curso_id", "curso_cod", metadataDocumento.curso_id);
            ViewBag.documento_id = new SelectList(db.Documento, "documento_id", "archivo", metadataDocumento.documento_id);
            ViewBag.persona_id = new SelectList(db.Persona, "persona_id", "dni", metadataDocumento.persona_id);
            ViewBag.semestre_id = new SelectList(db.Semestre, "semestre_id", "nombre", metadataDocumento.semestre_id);
            ViewBag.tipodocumento_id = new SelectList(db.TipoDocumento, "tipodocumento_id", "extension", metadataDocumento.tipodocumento_id);
            ViewBag.id_unidad = new SelectList(db.Unidad, "id_unidad", "descripcion", metadataDocumento.id_unidad);
            return View(metadataDocumento);
        }

        // POST: User/MetadataDocumentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "metadata_id,documento_id,curso_id,persona_id,semestre_id,tipodocumento_id,id_unidad,pagina_total,palabra_total,caracter_total,linea_total,parrafo_total,celda,columna,diapositiva,tamanio,programa_nombre,fecha_creacion,fecha_subida")] MetadataDocumento metadataDocumento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metadataDocumento).State = EntityState.Modified;
                db.SaveChanges();
                var cursoDocente = db.CursoDocente
                    .SingleOrDefault(x => x.persona_id == metadataDocumento.persona_id &&
                                          x.curso_id == metadataDocumento.curso_id).cursodocente_id;
                return RedirectToAction("Index", new { id = cursoDocente });
            }
            ViewBag.curso_id = new SelectList(db.Curso, "curso_id", "curso_cod", metadataDocumento.curso_id);
            ViewBag.documento_id = new SelectList(db.Documento, "documento_id", "archivo", metadataDocumento.documento_id);
            ViewBag.persona_id = new SelectList(db.Persona, "persona_id", "dni", metadataDocumento.persona_id);
            ViewBag.semestre_id = new SelectList(db.Semestre, "semestre_id", "nombre", metadataDocumento.semestre_id);
            ViewBag.tipodocumento_id = new SelectList(db.TipoDocumento, "tipodocumento_id", "extension", metadataDocumento.tipodocumento_id);
            ViewBag.id_unidad = new SelectList(db.Unidad, "id_unidad", "descripcion", metadataDocumento.id_unidad);
            return View(metadataDocumento);
        }

        // GET: User/MetadataDocumentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetadataDocumento metadataDocumento = db.MetadataDocumento.Find(id);
            if (metadataDocumento == null)
            {
                return HttpNotFound();
            }
            return View(metadataDocumento);
        }

        // POST: User/MetadataDocumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MetadataDocumento metadataDocumento = db.MetadataDocumento.Find(id);
            db.MetadataDocumento.Remove(metadataDocumento);
            db.SaveChanges();
            var cursoDocente = db.CursoDocente
                .SingleOrDefault(x => x.persona_id == metadataDocumento.persona_id &&
                                      x.curso_id == metadataDocumento.curso_id).cursodocente_id;
            return RedirectToAction("Index", new { id = cursoDocente });
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
