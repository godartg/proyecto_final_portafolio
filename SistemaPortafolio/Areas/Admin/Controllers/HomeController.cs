using System;
using System.Collections.Generic;
using System.Linq;
using SistemaPortafolio.Filters;
using SistemaPortafolio.CSOneDriveAccess;
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

        // GET: Admin/Home
        private string token = "EwCAA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAYR4Z/xhqJw0r3zNcmxYD+KXAuf20NStmih/i3N0Go7M7IPWJFhcxL6FpfNWNmQwdlhsn+YVZJkk6nhPkmqrubQUU9NZJuHFVTKZp7UoQL56gmLxbXGzrAWq7F8Z3X6ZRDSse5eOmAu7oJ+BTQpNNIHBD1QeuIKYoLeY0JXZktKYQW8MZXHCjPuHEp7ao6dHya+F0/U3ioaz2lU0qf7/tI0XUxgLKIqmvuq8LUmsMR4RJSbgBg5ORCRIkiP7r60OWHfS/FwBj9lyoNSMb1vlpkRRkfMl4z41NAtlKbUxkUOA93GTOcqhB4cS9d4AwnYl3KsRQo6NIDii4BlOAD/XeZ4DZgAACHFw8Nk4P7ltUAILB+ie4Tr7zg6ch7QqGlPc7qT36lhrMnDKs8Pb3HheKlDHwFFNl7VboBkukReXNmXwZc1ki0PMnVY5Nr7VOILGTOezZZYdo/VG8Ylvey9+QtxcvrCJJ3Rm5NXMUB3bEHhGfb5gpbUilkv5HYMw/rrfZ5pKcdotmSSxQwkJbe+PnvQRNhefHyBZNnnmiQCloSTQNdPSQRT2TOvW93L7RbUyde5UIAqgPbiPHxAElvfv3Z05KermqM405GlUdaT65T+hlN7XeAsfaeMG0ZtHHPht4GGnJjwTEejLKSwS4iqVj4cIWHmlm9UwXYYsAlc+FWUGJpwVdgiEm/bRoc0Om36RDx85cM7NX4Q5USnhq5B+AKZQgu8US3ejnnOqbXYnayQhbwSeMGic8JwpUkcbIC8OHnSPrj1sgqt+rRWFEJeAAh0KfBjtGNwe4g0sH+7gTw0hH1lhXcbn8zbfKzGGdCbMwQA+ZGn4XVplO8boDg7sksGxO/0LSHTDpipcx6UobCbly/cqFCP8pZVdNrcUYB2y8tl7Erd4j516lO3PYbeF+CrjhOwKK2j4BNjHLH2rwXjA0LMD8eq4x55jw+2NxTaxgaFRnUmRk6E90KDgf5Hv3DH4xjnj4ZQ52EGW01p4GaDeGBCuR6d8mpCWgwhlWCei2CJGW8EJ+WAlXzzJZswUBcvhDVvZ+93zFNaCN3IVj8VM1o/EBimiqPyI+9cbe9ZVocQox9XD5Pn1D79c1FCAOPxh2pjNW96F9wMZu+Eh56shsoKCOWqD/FM1Xtib610CgwI=";

        public async Task<ActionResult> Index()
        {
            var archivos = await listarAsync();
            //List<ClsCarpeta> listaCarpetas = LeerArchivos();
            //realizarComparacion(archivos, listaCarpetas);
            return View();
        }
        
        

        public async Task<ActionResult> Ver(string id, string nombre)
        {
            var archivos = await listarAsync(id);
            //List<ClsCarpeta> listaCarpetas = LeerArchivos();
            //realizarComparacion(archivos,listaCarpetas);
            return View("Index", archivos);
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
        public async Task<ActionResult> Index2()
        {
            //if user is not login, redirect to office 365 for authenticate
            if (string.IsNullOrEmpty(OfficeAccessSession.AccessCode))
            {
                string url = OfficeAccessSession.GetLoginUrl("onedrive.readwrite");

                return new RedirectResult(url);
            }
            
            return View();
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

            return View();
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
        public async Task CrearCarpeta(string nombre)
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

            //return await Index();

        }

    }
}
