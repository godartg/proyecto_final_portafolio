﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;
using SistemaPortafolio.Filters;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    [Autenticado]
    public class AlumnoController : Controller
    {
        Persona alumno = new Persona();
        // GET: Admin/Alumno
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CargarGrilla(AnexGRID grid)
        {
            return Json(alumno.ListarGrillaA(grid));
        }

        public ActionResult Curso(int id)
        {
            Session["idd_persona"] = id;
            Curso curso = new Curso();
            PlanEstudio planEstudio = new PlanEstudio();
            string anoActual = DateTime.Now.Year+"";
            int  mesActual = DateTime.Now.Month;
            string nombre_planEstudio = "Plan de Estudio "+anoActual;
            if(mesActual < 4)
            {
                nombre_planEstudio += "-R";
            }else if(mesActual >= 4){
                nombre_planEstudio += "-I";
            }
            else
            {
                nombre_planEstudio += "-II";
            }
            var _planEstudio = (from pe in planEstudio.listar() where pe.nombre == nombre_planEstudio select pe).FirstOrDefault() ;
            if(_planEstudio!= null)
            {
                ViewBag.cursos = curso.listarcurso(_planEstudio.plan_id);
            }
            else
            {
                _planEstudio = (from pe in planEstudio.listar() select pe).LastOrDefault();
            }
            ViewBag.cursoalumno = curso.cursoal(id);

            
            return View();
        }

        public JsonResult AgregarCurso(int[] codigo = null, string[] persona = null)
        {
            var rm = new ResponseModel();

            var curso_id = 0;
            int personaa = 0;

            foreach(var c in codigo)
            {
                curso_id = c;
            }
            foreach(var p in persona)
            {
                personaa = Convert.ToInt32(p);
            }

            Session["idd_personaa"] = personaa.ToString();

            rm = alumno.agregarcurso(curso_id, personaa);

            if (rm.response)
            {
                rm.href = Url.Content("/Admin/Alumno/Curso/" + personaa);
                return Json(rm);
            }
            else
            {
                return Json(rm);
            }
        }

        public ActionResult EliminarCurso(int id)
        {
            alumno.eliminarcurso(id);
            int idpersona = Convert.ToInt32(Session["idd_personaa"].ToString());
            return Redirect("~/Admin/Alumno/Curso/" + idpersona);
        }
    }
}