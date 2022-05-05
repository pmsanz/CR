using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ModelViewPaginaPrincipal
    {

        public List<PedidoClienteDashViewModel> dashPedidoCliente { get; set; }

        public List<PRODUCTOS> dashProductos { get; set; }

    }
}