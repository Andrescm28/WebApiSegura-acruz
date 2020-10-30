using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Equipaje
    {
        public int EQUI_ID { get; set; }
        public string EQUI_TIPO { get; set; }
        public int EQUI_PRECIO { get; set; }
        public string EQUI_DESCRIPCION { get; set; }
        
    }
}