using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class ModelViewCrearFactura
    {
        public IEnumerable<SelectListItem> tiposFacturacion { get; set; }

        public IEnumerable<SelectListItem> usuarios { get; set; }
        
        public List<TIPO_PAGOS> tipoPago { get; set; }
        
        
        [Required]
        public CLIENTES clientes { get; set; }

    }
}