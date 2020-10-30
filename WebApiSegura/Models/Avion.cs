using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Avion
    {
        public int AVI_ID { get; set; }
        public int AVI_CODIGO_AVION { get; set; }
        public string AVI_TIPO { get; set; }
        public string AVI_DESCRIPCION { get; set; }
    }
}