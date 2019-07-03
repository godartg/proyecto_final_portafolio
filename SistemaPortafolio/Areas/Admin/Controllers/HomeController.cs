using System;
using System.Collections.Generic;
using System.Linq;
using SistemaPortafolio.Filters;
using SistemaPortafolio.CSOneDriveAccess;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using SistemaPortafolio.Models;

namespace SistemaPortafolio.Areas.Admin.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        /// <summary>
        /// clientId of you office 365 application, you can find it in https://apps.dev.microsoft.com/
        /// </summary>
        private const string ClientId = "043f2159-507a-4e5f-ae85-60feff49a541";
        /// <summary>
        /// Password/Public Key of you office 365 application, you can find it in https://apps.dev.microsoft.com/
        /// </summary>
        private const string Secret = "hdwHIKYN289);beikUI84^#";
        /// <summary>
        /// Authentication callback url, you can set it in https://apps.dev.microsoft.com/
        /// </summary>
        private const string CallbackUri = "http://localhost:1438/Home/OnAuthComplate";
        // GET: Admin/Home
        /*
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(OfficeAccessSession.AccessCode))
            {
                string url = OfficeAccessSession.GetLoginUrl("onedrive.readwrite");

                return new RedirectResult(url);
            }
            return RedirectToAction("UploadFileAndGetShareUri");
        }*/
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// OfficeAccessSession object in session
        /// </summary>
        /*
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
        public async Task<ActionResult> UploadFileAndGetShareUri()
        {

            var file;
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
        }*/

    }
}