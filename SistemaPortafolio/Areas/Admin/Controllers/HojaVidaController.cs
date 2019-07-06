using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaPortafolio.Models;
using SistemaPortafolio.Filters;
using Wired.Razor;
using Wired.RazorPdf;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using SistemaPortafolio.CSOneDriveAccess;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    [Autenticado]
    public class HojaVidaController : Controller
    {
        HojaVida hojavida = new HojaVida();
        HojaVidaDocenteFA hojavidadocentefa = new HojaVidaDocenteFA();
        HojaVidaDocenteFC hojavidadocentefc = new HojaVidaDocenteFC();
        HojaVidaDocenteEX hojavidadocenteex = new HojaVidaDocenteEX();
        HojaVidaDocenteCRP hojavidadocentecrp = new HojaVidaDocenteCRP();
        HojaVidaDocenteActividadesDesarrolloProfesional hojavidadocenteadp = new HojaVidaDocenteActividadesDesarrolloProfesional();
        HojaVidaDocenteActividadServicios hojavidadocenteas = new HojaVidaDocenteActividadServicios();
        HojaVidaDocenteHonoresPremios hojavidadocentehp = new HojaVidaDocenteHonoresPremios();
        HojaVidaDocenteMembresia hojavidadocentem = new HojaVidaDocenteMembresia();
        HojaVidaDocentePublicaciones hojavidadocentep = new HojaVidaDocentePublicaciones();
        Usuario usuario = new Usuario().Obtener(SessionHelper.GetUser());
        Documento documento = new Documento();
        Parser parser = new Parser();

        // GET: Admin/HojaVida
        public async Task<ActionResult> Index()
        {
            return View(hojavida.Listar());
        }
        
        public ActionResult MiHojaVida()
        {
            int hojavida_id = ObtenerHojaVidaId(0);

            List<HojaVida> hojas = new List<HojaVida>();

            hojas.Add(hojavida.Obtener(hojavida_id));

            return View(hojas);
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
        [HttpPost]
        public async Task<ActionResult> UploadFileAndGetShareUri(HttpPostedFileBase file)
        {
            //save upload file to temp dir in local disk
            var path = Path.GetTempFileName();
            file.SaveAs(path);

            //upload the file to oneDrive and get a file id
            string oneDrivePath = file.FileName;

            string result = await OfficeAccessSession.UploadFileAsync(path, oneDrivePath);

            JObject jo = JObject.Parse(result);
            string fileId = jo.SelectToken("id").Value<string>();

            //request oneDrive REST API with this file id to get a share link
            string shareLink = await OfficeAccessSession.GetShareLinkAsync(fileId, OneDriveShareLinkType.embed, OneDrevShareScopeType.anonymous);

            ViewBag.ShareLink = shareLink;

            return View();
        }

        public async Task<ActionResult> Imprimir(int persona_id=0)
        {

            Documento doc = new Documento();
            TipoDocumento tipoDocumento = new TipoDocumento();
            int hojavida_id = ObtenerHojaVidaId(persona_id);

            HojaVida hoja = new HojaVida();
            
            hoja = hojavida.Obtener(hojavida_id);

            ViewData["hojavidadocentefa"] = hojavidadocentefa.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentefc"] = hojavidadocentefc.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocenteex"] = hojavidadocenteex.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentecrp"] = hojavidadocentecrp.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocenteadp"] = hojavidadocenteadp.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocenteas"] = hojavidadocenteas.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentehp"] = hojavidadocentehp.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentem"] = hojavidadocentem.Listar(usuario.Persona.persona_id);
            ViewData["hojavidadocentep"] = hojavidadocentep.Listar(usuario.Persona.persona_id);
            var generator = new MvcGenerator(ControllerContext);
            var pdf = generator.GeneratePdf(hoja, "Imprimir");
            documento.persona_id = usuario.Persona.persona_id;
            documento.tipodocumento_id = 1;
            documento.descripcion = "Curriculum Vitae ICACIT";
            documento.estado = "activo";
            documento.GuardarArchivoDirecto(pdf, usuario.Persona.persona_id, "HojaDeVida.pdf", "Curriculum Vitae ICACIT");
            var archivo_ruta = Path.Combine(Server.MapPath("~/Server/Docs/Curriculum Vitae ICACIT/"), Path.GetFileName("HojaVida.pdf"));
            //HttpPostedFileBase objFile = (HttpPostedFileBase)new MemoryPostedFile(pdf);
            

            string result = await OfficeAccessSession.UploadFileAsync(archivo_ruta, "Server/Docs/Curriculum Vitae ICACIT/HojaVida.pdf");


            
            //HttpPostedFileBase 
            return new FileContentResult(pdf, "application/pdf");
            //return View();
        }
        /**
        public async Task<ActionResult> DirectUpload()
        {
            //if user is not login, redirect to office 365 for authenticate
            if (string.IsNullOrEmpty(OfficeAccessSession.AccessCode))
            {
                string url = OfficeAccessSession.GetLoginUrl("onedrive.readwrite");

                return new RedirectResult(url);
            }

            string result = await OfficeAccessSession.UploadFileAsync("C:\Users\Drei\Dropbox\UPT\eXtra Work\proyecto_final_portafolio\SistemaPortafolio\Server\Docs\Curriculum Vitae ICACIT\hojaVida.pdf", "ojio.pdf");


            return View();
        }*/
        public async void SubirArchivo(string nombre, byte[] pdf)
        {
            var archivo_ruta = Path.Combine(Server.MapPath("~/Server/Docs/Curriculum Vitae ICACIT/"), Path.GetFileName(nombre));

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.token);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "octet-stream");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri("https://graph.microsoft.com/v1.0/me/drive/root:" + nombre + ":/content"));

                request.Content = new ByteArrayContent(pdf);

                var respuesta = await httpClient.SendAsync(request);
                //respuesta.StatusCode.ToString()
            }
            
        }

        private byte[] ReadFileContent(string filePath)
        {
            using (FileStream inStrm = new FileStream(filePath, FileMode.Open))
            {
                byte[] buf = new byte[2048];
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    int readBytes = inStrm.Read(buf, 0, buf.Length);
                    while (readBytes > 0)
                    {
                        memoryStream.Write(buf, 0, readBytes);
                        readBytes = inStrm.Read(buf, 0, buf.Length);
                    }
                    return memoryStream.ToArray();
                }
            }
        }
        public int ObtenerHojaVidaId(int id = 0)
        {
            if (id <= 0)
            {

                hojavida = hojavida.ObtenerByPersona(usuario.persona_id);

                if (hojavida == null)
                {
                    hojavida = new HojaVida();
                    hojavida.persona_id = usuario.persona_id;
                    hojavida.Guardar();
                    id = hojavida.hojavida_id;
                }
                else
                {
                    id = hojavida.hojavida_id;
                }
            }
            else
            {
                hojavida = hojavida.ObtenerByPersona(id);
                id = hojavida.hojavida_id;
            }
            return id;
        }

        [HttpGet]
        public ActionResult AgregarEditarFA(int id = 0)
        {
            if (id > 0)
            {
                hojavidadocentefa = hojavidadocentefa.Obtener(id);
            }
            else
            {
                hojavidadocentefa.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentefa.Listar(usuario.Persona.persona_id);


            return View(hojavidadocentefa);
        }

        [HttpPost]
        public ActionResult AgregarEditarFA(HojaVidaDocenteFA model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarFA");
        }

        //AgregarEditarFC

        [HttpGet]
        public ActionResult AgregarEditarFC(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentefc = hojavidadocentefc.Obtener(id);
            }
            else
            {
                hojavidadocentefc.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentefc.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentefc);
        }

        [HttpPost]
        public ActionResult AgregarEditarFC(HojaVidaDocenteFC model)
        {

            model.hojavida_id = 2;

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarFC");
        }

        //AgregarEditarEX
        [HttpGet]
        public ActionResult AgregarEditarEX(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocenteex = hojavidadocenteex.Obtener(id);
            }
            else
            {
                hojavidadocenteex.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocenteex.Listar(usuario.Persona.persona_id);

            return View(hojavidadocenteex);
        }

        [HttpPost]
        public ActionResult AgregarEditarEX(HojaVidaDocenteEX model)
        {

            model.fechainicio = (DateTime)model.fechainicio;
            model.fechafin = (DateTime)model.fechafin;
            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarEX");
        }

        //AgregarEditarCRP
        [HttpGet]
        public ActionResult AgregarEditarCRP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentecrp = hojavidadocentecrp.Obtener(id);
            }
            else
            {
                hojavidadocentecrp.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentecrp.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentecrp);
        }

        [HttpPost]
        public ActionResult AgregarEditarCRP(HojaVidaDocenteCRP model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarCRP");
        }
        //AgregarEditarP
        [HttpGet]
        public ActionResult AgregarEditarP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentep = hojavidadocentep.Obtener(id);
            }
            else
            {
                hojavidadocentep.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentep.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentep);
        }

        [HttpPost]
        public ActionResult AgregarEditarP(HojaVidaDocentePublicaciones model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarP");
        }
        //AgregarEditarADP
        [HttpGet]
        public ActionResult AgregarEditarADP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocenteadp = hojavidadocenteadp.Obtener(id);
            }
            else
            {
                hojavidadocenteadp.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocenteadp.Listar(usuario.Persona.persona_id);

            return View(hojavidadocenteadp);
        }

        [HttpPost]
        public ActionResult AgregarEditarADP(HojaVidaDocenteActividadesDesarrolloProfesional model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarADP");
        }
        //AgregarEditarAS
        [HttpGet]
        public ActionResult AgregarEditarAS(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocenteas = hojavidadocenteas.Obtener(id);
            }
            else
            {
                hojavidadocenteas.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocenteas.Listar(usuario.Persona.persona_id);

            return View(hojavidadocenteas);
        }

        [HttpPost]
        public ActionResult AgregarEditarAS(HojaVidaDocenteActividadServicios model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarAS");
        }
        //AgregarEditarHP
        [HttpGet]
        public ActionResult AgregarEditarHP(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentehp = hojavidadocentehp.Obtener(id);
            }
            else
            {
                hojavidadocentehp.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentehp.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentehp);
        }

        [HttpPost]
        public ActionResult AgregarEditarHP(HojaVidaDocenteHonoresPremios model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarHP");
        }
        //AgregarEditarM
        [HttpGet]
        public ActionResult AgregarEditarM(int id = 0)
        {

            if (id > 0)
            {
                hojavidadocentem = hojavidadocentem.Obtener(id);
            }
            else
            {
                hojavidadocentem.hojavida_id = ObtenerHojaVidaId(id);
            }

            ViewData["listado"] = hojavidadocentem.Listar(usuario.Persona.persona_id);

            return View(hojavidadocentem);
        }
        
        [HttpPost]
        public ActionResult AgregarEditarM(HojaVidaDocenteMembresia model)
        {

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Redirect("~/Admin/HojaVida/AgregarEditarM");
        }
        
    }
}