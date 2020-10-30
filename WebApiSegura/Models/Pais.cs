using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Pais
    {
        public int PAI_ID { get; set; }
        public int PAI_CODIGO_PAIS { get; set; }
        public string PAI_TIPO { get; set; }
        public string PAI_DESCRIPCION { get; set; }
    }
}