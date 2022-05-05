using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {

        private CREntities1 db = new CREntities1();

        public ActionResult Index()
        {
            ModelViewPaginaPrincipal pag = new ModelViewPaginaPrincipal();
            pag.dashPedidoCliente = new List<PedidoClienteDashViewModel>();
            pag.dashProductos = new List<PRODUCTOS>();

            pag.dashProductos = db.PRODUCTOS.ToList();

            var pedidos_clientes = db.PEDIDOS_CLIENTES.Include(p => p.AspNetUsers).Include(p => p.CLIENTES).Include(p => p.ESTADOS_PEDIDOS).Include(x => x.PEDIDOS_CLIENTES_DETALLES);
            

            foreach (var item in pedidos_clientes)
            {
                PedidoClienteDashViewModel pcd = new PedidoClienteDashViewModel();
                pcd.PedidoID = item.PedidoClienteID;
                pcd.AspNetUsers = item.AspNetUsers;
                pcd.CLIENTES = item.CLIENTES;
                pcd.ESTADOS_PEDIDOS = item.ESTADOS_PEDIDOS;
                pcd.FechaCreacion = item.FechaCreacion;


                var totalPrecio = item.PEDIDOS_CLIENTES_DETALLES.Sum(x => x.PrecioUnitario * x.Cantidad);
                pcd.Total = totalPrecio;

                pag.dashPedidoCliente.Add(pcd);
            }

            



            return View(pag);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Detalle(string keyword)
        {

            var id = keyword;

            return RedirectToAction("PedidoCliente", "Edit", new {id});
        }

        public ActionResult Eliminar(string keyword)
        {
            int id = Convert.ToInt32(keyword);

            PEDIDOS_CLIENTES ped = db.PEDIDOS_CLIENTES.Find(id);

            if (ped != null)
            {
                db.PEDIDOS_CLIENTES.Remove(ped);
                db.SaveChanges();
            }

            //ViewBag.Message = "Your contact page.";

            return RedirectToAction("Index");
        }
    }
}