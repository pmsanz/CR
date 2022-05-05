using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class ModelViewEditFactura
    {
        public int PedidoClienteID { get; set; }
        public CLIENTES cliente { get; set; }
        public SelectList usuarios { get; set; }
        public ModelViewFacturacionYPago fyp { get; set; }
        public List<ProductosJqueryViewModel> productos { get; set; }

    }
}