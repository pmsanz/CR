
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebApplication3.Models
{

using System;
    using System.Collections.Generic;
    
public partial class PEDIDOS_PROVEEDORES_DETALLES
{

    public int PPDetalleID { get; set; }

    public int PedidoProveedorID { get; set; }

    public string Detalle { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public int ProductoID { get; set; }



    public virtual PEDIDOS_PROVEEDORES PEDIDOS_PROVEEDORES { get; set; }

    public virtual PRODUCTOS PRODUCTOS { get; set; }

}

}
