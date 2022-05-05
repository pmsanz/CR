using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class ModelViewMovimientoCuentaCliente
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public  DateTime fechaPago;
              
        public SelectList cuentas { get; set; }

        public CLIENTES cliente { get; set; }

        public SelectList tipo { get; set; }

        List<Tipo> tipos = new List<Tipo>();

        public ModelViewFacturacionYPago fyp { get; set; }

        public int? referenciaPedido { get; set; }

        public String detalle { get; set; }

        public class Tipo {
            public int id { get; set; }
            public string value { get; set; }
        }

        public ModelViewMovimientoCuentaCliente(int selected)
        {
            
            if (this.tipos.Count == 0 && selected == null)
            {
                this.tipos.Add(new Tipo() { id = 1, value = "Debito" });
                this.tipos.Add(new Tipo() { id = 2, value = "Credito" });
                this.tipo = new SelectList(this.tipos, "id", "value");
            }
            else if (this.tipos.Count != 0 && selected != null)
            {
                foreach (var item in this.tipo)
                {
                    if (item.Value == selected.ToString())
                        item.Selected = true;
                }
            }
            else {
                this.tipos.Add(new Tipo() { id = 1, value = "Debito" });
                this.tipos.Add(new Tipo() { id = 2, value = "Credito" });
                this.tipo = new SelectList(this.tipos, "id", "value");
                foreach (var item in this.tipo)
                {
                    if (item.Value == selected.ToString())
                        item.Selected = true;
                }
            }

            

        }

    }
}