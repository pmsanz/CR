using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ProductosJqueryViewModel
    {
        
            public int productoID { get; set; }
            public string descripcion { get; set; }
            public string cantidad { get; set; }
            public decimal precio { get; set; }
            public decimal subTotal { get; set; }
        

    }
}