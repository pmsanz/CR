using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PedidoClienteDashViewModel
    {

        public int PedidoID { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Decimal Total { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual CLIENTES CLIENTES { get; set; }
        public virtual ESTADOS_PEDIDOS ESTADOS_PEDIDOS { get; set; }

    }
}