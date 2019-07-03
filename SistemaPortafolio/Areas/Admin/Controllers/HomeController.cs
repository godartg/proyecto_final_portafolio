using System;
using System.Collections.Generic;
using System.Linq;
using SistemaPortafolio.Filters;
//using SistemaPortafolio.CSOneDriveAccess;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SistemaPortafolio.Models;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        /// <summary>
        /// clientId of you office 365 application, you can find it in https://apps.dev.microsoft.com/
        /// </summary>
        private const string ClientId = "6ac7393c-901a-49f0-9e89-316fe751a0a8";
        /// <summary>
        /// Password/Public Key of you office 365 application, you can find it in https://apps.dev.microsoft.com/
        /// </summary>
        private const string Secret = "nNLT0-@csodnsDCAE1785#}";
        /// <summary>
        /// Authentication callback url, you can set it in https://apps.dev.microsoft.com/
        /// </summary>
        private const string CallbackUri = "http://localhost:49204/Admin/Home/OnAuthComplate";
        // GET: Admin/Home
        
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(OfficeAccessSession.AccessCode))
            {
                string url = OfficeAccessSession.GetLoginUrl("onedrive.readwrite");

                return new RedirectResult(url);
            }
            return RedirectToAction("UploadFileAndGetShareUri");
        }
        public ActionResult Index2()
        {
            return View();
        }
        
        /// <summary>
        /// OfficeAccessSession object in session
        /// </summary>
        
        public O365RestSession OfficeAccessSession
        {
            get
            {
                var officeAccess = Session["OfficeAccess"];
                if (officeAccess == null)
                {
                    officeAccess = new O365RestSession(ClientId, Secret, CallbackUri);
                    Session["OfficeAccess"] = officeAccess;
                }
                return officeAccess as O365RestSession;
            }
        }


        //when user complate authenticate, will be call back this url with a code
        public async Task<RedirectResult> OnAuthComplate(string code)
        {
            await OfficeAccessSession.RedeemTokensAsync(code);

            return new RedirectResult("Index");
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

            return RedirectToAction("Index2");
        }
        public void LeerArchivos()
        {
            var path = Server.MapPath(@"~/Server/");
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            List<ClsCarpeta> listCarpetas = new List<ClsCarpeta>();
            List<ClsArchivo> listArchivos;
            List<ClsCarpeta> listSubCarpetas;
            ClsArchivo archivo;
            ClsCarpeta subCarpeta;
            foreach (var item in directoryInfo.GetDirectories())
            {

                listSubCarpetas = new List<ClsCarpeta>();
                foreach (var item2 in item.GetDirectories())
                {
                    subCarpeta = new ClsCarpeta();
                    listArchivos = new List<ClsArchivo>();

                    foreach (var item3 in item.GetFiles())
                    {
                        archivo = new ClsArchivo();
                        archivo.nombreFile = item3.Name;
                        archivo.link = item3.FullName;
                        listArchivos.Add(archivo);
                    }
                    subCarpeta.nombreCarpeta = item2.Name;
                    subCarpeta.link = item2.FullName;
                    listSubCarpetas.Add(subCarpeta);

                }

                listCarpetas.Add(new ClsCarpeta()
                {
                    nombreCarpeta = item.Name,
                    link = item.FullName,
                    Carpetas = listSubCarpetas
                });


            }
        }
        private string token = "EwCAA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAXSl/29y9GoxxJbc/yuR9/o9Umk6wJYW55xJTgCHfVIaDiqT/0iY0AiTTzYgtwv9mDbsmKH/nthi/0PxVOY4ImvhBDP1qW7+tpUZdNv9CcD5pY5OMaMqnCSjXQyO/nicVbqd6L/sQyhSwV0Q57macMxuaZ4I00+FicUZswqPIw2uXbE7w0Rt8nISm4vAQ5AiTl8xF0UuJdwxRwkII/VD/nFydyFErzFJClZp9MgfH4FCg4VScx8g4qYBNbKs2AyTFsoxfdgyWlofe1ZHEsC+jmawV+oXynYYiYM84Yy79C3iR3XARqrNWtlXr3QVOJFeCvYDcFKcVNPdvpvZGpLNcGMDZgAACHZGJgV0xQAlUAJp9JO6ebYwHEcNeSshNSVxKT1eUAQCofofEhnBoG5XgBFANxtQZUwyfm521i1wXoZex2uR+WGHSD8MZS4Vf+rVK136yA5o4Jtf7WrRMl+0HuWjHssCWsqhrCTrZu1OgqHxMVzCsq70SejV/w66QMzfuPthxzbywkFP7kRq5LB4iSEfPuCRamqZHUFvEEpGH7qlXTisClDHv/63kr0KR/EJlk3AjLc4j2DjDWLUKfM/fCpWImf4IdI0z0R80UYPs60ZawQecFFFNq1B09Dmu0qZ1clW5q956q2rqPQutkzZM7unRDPEMw+u7picxKZJAagNgQoN9yPAPrzSrofw/N5TlsdIHRn4y5OyTklmXcFtxmzGJ1rgfD/kdIs6JfGx9waNSBw6MH6g6i8J4HBZMBcOY3DPVe5IHmPuLOJE3x4cp84QAcABtTdnmw1Qce+ToqtKla1yQeTAZszvcJx4QVHWFOiz2DAWdDFsBKGGjUSBOfIuvJMR7Hu9AlMEicwcWtH21zscLY3WOYzKXF7S0K9XPmot33VBPLNZvi+MW2RkzQ01Impr1ENGVbaiwoxCDS/PT8J12lF/XrqRDYgrTrtInPkR6rtQz712/e9oea1Q0Ip+TqxbuLRxm08payj4s9iVg87Grz0HCFEzanlGj7E0fSflvS6WbJdHxX2CxKCj6Uk9L12n1TvTMAs826bixueVNEikR9oeLRDPfpopWK64iLQ2cyr1oNGACAKpxER6ft69pcGbO3HgPLnDDe4jm+CM+zNuUAYB0GmAgYd3SkrVgwI=";

        // GET: Archivos
        public async Task<ActionResult> Index3()
        {
            var archivos = await listarAsync();

            return View("Index3", archivos);
        }

        public async Task<ActionResult> Ver(string id)
        {
            var archivos = await listarAsync(id);

            return View("Index3", archivos);
        }

        private async Task<Archivos> listarAsync()
        {
            Archivos archivos = new Archivos();

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://graph.microsoft.com/v1.0/me/drive/root/children"));

                var respuesta = await httpClient.SendAsync(request);
                var contenido = respuesta.Content.ReadAsStringAsync().Result;

                archivos = JsonConvert.DeserializeObject<Archivos>(contenido);
            }

            return archivos;
        }

        private async Task<Archivos> listarAsync(string id)
        {
            Archivos archivos = new Archivos();

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://graph.microsoft.com/v1.0/me/drive/items/" + id + "/children"));

                var respuesta = await httpClient.SendAsync(request);
                var contenido = respuesta.Content.ReadAsStringAsync().Result;

                archivos = JsonConvert.DeserializeObject<Archivos>(contenido);
            }

            return archivos;
        }

        [HttpPost]
        public async Task<ActionResult> CrearCarpeta(string nombre)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://graph.microsoft.com/v1.0/me/drive/root/children"));

                request.Content = new StringContent("{\"name\":\"" + nombre + "\",\"folder\":{}}",
                                    Encoding.UTF8,
                                    "application/json");

                var respuesta = await httpClient.SendAsync(request);
                var contenido = respuesta.Content.ReadAsStringAsync().Result;

            }

            return await Index3();

        }

        [HttpPost]
        public async Task<ActionResult> SubirArchivo(string nombre)
        {
            var archivo_ruta = "";
            var archivo_nombre = "";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "octet-stream");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://graph.microsoft.com/v1.0/me/drive/root:/" + archivo_nombre + ":/content"));

                request.Content = new ByteArrayContent(ReadFileContent(archivo_ruta));

                var respuesta = await httpClient.SendAsync(request);
                //respuesta.StatusCode.ToString()
            }

            return await Index3();

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

    }
}
