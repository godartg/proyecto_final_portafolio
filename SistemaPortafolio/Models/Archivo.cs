using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    public class Archivo
    {
        public string id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string createdDateTime { get; set; }
        public string lastModifiedDateTime { get; set; }
        public Folder folder { get; set; }
    }

    public class Folder
    {
        public int childCount { get; set; }
    }
}