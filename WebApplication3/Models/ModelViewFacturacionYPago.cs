using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class ModelViewFacturacionYPago
    {
        public SelectList tiposFacturacion { get; set; }
        public SelectList tiposPago { get; set; }

    }
}