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
        private string token = "EwCAA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAQGikB9tfwC1qwiGanoXCjzS86qFNTGjqlPknMuA7kzIbWmTUs9rLzIOiXP4mbv5O4DwtXqZ2GUyEvl1oT1vsFbHmraI6c9zD5UbLjLdzzx35yjL4ALQAv6wALMJDLOkCTczgDVLiQs9FgYbOEBDHOj7CmlGwW077yy7aqd0Zw2fABIorj53xgVPWLv0GYKKp5vrK8S2QTCuhnQ+08634ru24pbwm+dYvxNWAfPLi37MVfpiKopsHnDgZnsrkzCnLIDyTLrLj9ZqV/u2d51tMYEMXQXs9GDddLtBBq0aYEso/5l40SVE1vDsOGgGQMbjgwwgm7CGnh8crQaXX2IiKp0DZgAACPJyO/7oijw0UAK66sJemEleDsrWQZ36RxZx6bGfCHLgZ8sNm7u+lnCaszIWUN1dXIUs/wYrPjt/Q73Zt0lQJ+ubmCGBImZm+ZPHRYP6VOqeGRgf5nfEzKTtBJFzJ7rZuf1vaHh9sVICaQAKC1Akd8QWXMCIw8CzK86+mUnj3sTiMsbKY4qqParUN99OpLcpJCgehlXmUbZSPzQESPDD8Qws4DWjfz2UI/whTnY7ci1RzZxvZKeh58zO5kgpdk1yzilWfPuM84Zeppt67WVMIsukhpx7R+vCzb6qmLKO83MRJcC3n9V5j7XpPHWUnItWVuCIJBU/BfoXtJpKSMWxCnqTyQwGlSbJ/IhFJmWvI65hDSbi9t+nqI939t0/FWLqjdJ4cNRg6Ic+cZ/c+Tg0nYddWo/Xm1keDdXMqy43bSohvwLfxnm58iFW5k8AHZ+/lBg4mHfxgVZpwqV67VdC6vSgoK+12zMDg5xCAt9Yyu8brJaFckI/bun+wgC+pxRURqz8wMIIgz4aF3xYtHo7zjCaIFw91t1fMuB8Azue7hm+Nzg0OQmSs8rj/GR0F9+kLgYEcHkmw9MuWiNOoq2qqNEswkxH7JxQRYytrv1isvm+w4HlqoCPA6Zqo4XT1PRhWE9e8ovq+cPWdn7SLYzLbOdAifT7Ihl+h4CFROHWYiRKrxciwtn9HZv7Gg4UrTXhs9xqG5ULJfoX287yu3zxh0XcvnEOOnXPI1oQEl5/lE4EvCGxb34E2fBsEAo1VnNHPii73NeT54b+CdBRBDSh29zpqV8b/locJ7RTgwI=";

        public async Task<ActionResult> Index()
        {
            var archivos = await listarAsync();
            List<ClsCarpeta> listaCarpetas = LeerArchivos();
            realizarComparacion(archivos, listaCarpetas);
            return View();
        }
        
        
        public List<ClsCarpeta> LeerArchivos(string ruta="")
        {
            var path = Server.MapPath(@"~/Server/"+ruta);
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

                    foreach (var item3 in item2.GetFiles())
                    {
                        archivo = new ClsArchivo();
                        archivo.nombreFile = item3.Name;
                        archivo.link = item3.FullName;
                        listArchivos.Add(archivo);
                    }
                    subCarpeta.Archivos = listArchivos;
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
            return listCarpetas;
        }
        



        private void realizarComparacion(Archivos archivos, List<ClsCarpeta> listaCarpetas)
        {
            
            if(listaCarpetas != null)
            {
                List<String> listaFolderNube = new List<String>();
                List<String> listaArchivoNube = new List<String>();
                List<String> listaDiferenciaFolder = new List<string>();
                if (archivos.value != null)
                {
                    foreach (var carpetNube in archivos.value)
                    {
                        if (carpetNube.folder != null)
                        {
                            listaFolderNube.Add(carpetNube.name);
                        }
                        else
                        {
                            listaArchivoNube.Add(carpetNube.name);
                        }

                    }
                }
                //Listar Recursos Locales
                List<String> listaFolderLocal = new List<String>();
                List<String> listaArchivoLocal = new List<String>();
                
                foreach (var carpetLocal in listaCarpetas)
                {
                    listaFolderLocal.Add(carpetLocal.nombreCarpeta);
                    if(carpetLocal.Archivos != null)
                    {
                        foreach (var archivosLocal in carpetLocal.Archivos)
                        {
                            listaArchivoLocal.Add(archivosLocal.nombreFile);
                        }
                    }
                }
                //Hallar Archivos del Folder local sin subir
                
                if (listaFolderNube!= null)
                {
                    listaDiferenciaFolder = listaFolderLocal.Except(listaFolderNube).ToList();
                }
                else
                {
                    listaDiferenciaFolder = listaFolderLocal.ToList();
                }
                
                string id_folder = "";
                if (listaDiferenciaFolder != null)
                {
                    foreach (var nombresCarpetas in listaDiferenciaFolder)
                    {
                        CrearCarpeta(nombresCarpetas);
                        id_folder = (from lista in archivos.value where lista.name == nombresCarpetas select lista.id).FirstOrDefault();
                        Ver(id_folder, nombresCarpetas);
                    }
                }
                else
                {

                }

                string archivoRuta = "";
                var listaDiferenciaArchivos = listaArchivoLocal.Except(listaArchivoNube).ToList();
                if (listaDiferenciaArchivos != null)
                {
                    foreach (var nombresArchivos in listaDiferenciaArchivos)
                    {
                        archivoRuta += nombresArchivos;
                        SubirArchivo(archivoRuta);
                    }
                }
            }
            //Listar Recursos de la nube
            
        }

        public async Task<ActionResult> Ver(string id, string nombre)
        {
            var archivos = await listarAsync(id);
            List<ClsCarpeta> listaCarpetas = LeerArchivos();
            realizarComparacion(archivos,listaCarpetas);
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

            return await Index();

        }

      
        public async Task<ActionResult> SubirArchivo(string ruta_file)
        {
            
            string archivo_ruta = Path.Combine(Server.MapPath("~/Server/"), Path.GetFileName(ruta_file));
            var archivo_nombre = Path.GetFileName(ruta_file);
            
            
            //archivo_nombre.SaveAs();

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "octet-stream");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, new Uri("https://graph.microsoft.com/v1.0/me/drive/root:/" + archivo_nombre + ":/content"));

                request.Content = new ByteArrayContent(ReadFileContent(archivo_ruta));

                var respuesta = await httpClient.SendAsync(request);
                //respuesta.StatusCode.ToString()
            }

            return await Index();

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
