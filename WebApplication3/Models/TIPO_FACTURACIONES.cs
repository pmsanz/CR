
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
    
public partial class TIPO_FACTURACIONES
{

    public TIPO_FACTURACIONES()
    {

        this.PEDIDOS_CLIENTES = new HashSet<PEDIDOS_CLIENTES>();

        this.PEDIDOS_PROVEEDORES = new HashSet<PEDIDOS_PROVEEDORES>();

        this.CLIENTES = new HashSet<CLIENTES>();

        this.PROVEEDORES = new HashSet<PROVEEDORES>();

    }


    public int TFacturacionID { get; set; }

    public string Descripcion { get; set; }



    public virtual ICollection<PEDIDOS_CLIENTES> PEDIDOS_CLIENTES { get; set; }

    public virtual ICollection<PEDIDOS_PROVEEDORES> PEDIDOS_PROVEEDORES { get; set; }

    public virtual ICollection<CLIENTES> CLIENTES { get; set; }

    public virtual ICollection<PROVEEDORES> PROVEEDORES { get; set; }

}

}
