using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Aeropuerto
    {
        public int AERO_ID { get; set; }
        public int AERO_CODIGO_AEROPUERTO { get; set; }
        public string AERO_TIPO { get; set; }
        public string AERO_DESCRIPCION { get; set; }
    }
}