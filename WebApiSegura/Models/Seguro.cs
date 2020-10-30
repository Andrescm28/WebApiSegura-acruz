using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Seguro
    {
        public int SEG_ID { get; set; }
        public string SEG_TIPO { get; set; }
        public int SEG_PRECIO { get; set; }
        public string SEG_DESCRIPCION { get; set; }
    }
}