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
    
     
    public class PedidoClienteController : Controller
    {
        private CREntities1 db = new CREntities1();
     //   private List<Models.ProductosJqueryViewModel> _lstproductos = new List<Models.ProductosJqueryViewModel>();
       

        // GET: /PedidoCliente/
        public ActionResult Index()
        {
            var pedidos_clientes = db.PEDIDOS_CLIENTES.Include(p => p.AspNetUsers).Include(p => p.CLIENTES).Include(p => p.ESTADOS_PEDIDOS).Include(x => x.PEDIDOS_CLIENTES_DETALLES);
            List<PedidoClienteDashViewModel> pc = new List<PedidoClienteDashViewModel>();

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

                pc.Add(pcd);
            }

            

            return View(pc);
        }


        // GET: /PedidoCliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDOS_CLIENTES pedidos_clientes = db.PEDIDOS_CLIENTES.Find(id);
            if (pedidos_clientes == null)
            {
                return HttpNotFound();
            }
            return View(pedidos_clientes);
        }

        // GET: /PedidoCliente/Create
        public ActionResult Create()
        {
            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre");
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre");
            return View();
        }

        // POST: /PedidoCliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PedidoClienteID,EPedidoID,ClienteID,AsignatarioID,Total")] PEDIDOS_CLIENTES pedidos_clientes)
        {
            if (ModelState.IsValid)
            {
                db.PEDIDOS_CLIENTES.Add(pedidos_clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email", pedidos_clientes.AsignatarioID);
            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre", pedidos_clientes.ClienteID);
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre", pedidos_clientes.EPedidoID);
            return View(pedidos_clientes);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerarPedido(FormCollection col)
        {

            var a = Request.Form["hddCliente"];
            var b = Request.Form["hddProductos"];
            var c = Request.Form["hddTotal"];
            var d = Request.Form["hddAsignatario"];
            
            // CONTADO
            var chk1 = Convert.ToBoolean(Request.Form["chk1"].Split(',')[0]);
            // CHEQUE
            var chk2 = Convert.ToBoolean(Request.Form["chk2"].Split(',')[0]);
            // MERCADERIA
            var chk3 = Convert.ToBoolean(Request.Form["chk3"].Split(',')[0]);

            var f = Request.Form["hddTipoFacturacion"].Trim();

                        
            bool MyBoolValue = Convert.ToBoolean(Request.Form["chk1"].Split(',')[0]);

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<Models.ProductosJqueryViewModel> _lstProductos = js.Deserialize<List<Models.ProductosJqueryViewModel>>(b);

                PEDIDOS_CLIENTES pc = new PEDIDOS_CLIENTES();
                pc.AsignatarioID = db.AspNetUsers.Where(x => x.UserName == d).FirstOrDefault().Id;
                pc.ClienteID = Convert.ToInt32(a);
                pc.EPedidoID = db.ESTADOS_PEDIDOS.Where(x => x.Nombre == "CREADO").Select(j => j.EPedidoID).FirstOrDefault();
                pc.Total = Convert.ToDecimal(c);
                pc.FechaCreacion = DateTime.Now;
                pc.FechaModificacion = DateTime.Now;
                pc.TFacturacionID = db.TIPO_FACTURACIONES.Find(Convert.ToInt32(f)).TFacturacionID;
                
                if (chk1){
                
                    TIPO_PAGOS tp = db.TIPO_PAGOS.Find(1);
                    pc.TIPO_PAGOS.Add(tp);
                
                }
                if (chk2)
                {

                    TIPO_PAGOS tp = db.TIPO_PAGOS.Find(2);
                    pc.TIPO_PAGOS.Add(tp);

                }
                if (chk3)
                {

                    TIPO_PAGOS tp = db.TIPO_PAGOS.Find(3);
                    pc.TIPO_PAGOS.Add(tp);

                }

                db.PEDIDOS_CLIENTES.Add(pc);
                db.SaveChanges();

                foreach (var item in _lstProductos)
                {
                    PEDIDOS_CLIENTES_DETALLES pd = new PEDIDOS_CLIENTES_DETALLES();
                    
                    pd.Cantidad = Convert.ToInt32(item.cantidad);
                    PRODUCTOS p = db.PRODUCTOS.Where(x => x.ProductoID == item.productoID).FirstOrDefault();
                    if (p != null)
                    {
                        pd.ProductoID = p.ProductoID;
                        pd.Detalle = p.Detalle;
                        pd.PrecioUnitario = item.precio;
                        pd.PedidoClienteID = pc.PedidoClienteID;
                        db.PEDIDOS_CLIENTES_DETALLES.Add(pd);
                        db.SaveChanges();
                    }
                    else
                        throw new Exception();
                                        
                }


            }
            catch (Exception)
            {
                
                throw;
            }

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarCambios(FormCollection col) {

            var a = Request.Form["hddCliente"];
            var b = Request.Form["hddProductos"];
            var c = Request.Form["hddTotal"];
            var d = Request.Form["hddAsignatario"];
            var e = Request.Form["hddPedidoID"];

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<Models.ProductosJqueryViewModel> _lstProductos = js.Deserialize<List<Models.ProductosJqueryViewModel>>(b);

                PEDIDOS_CLIENTES pc = db.PEDIDOS_CLIENTES.Find(Convert.ToInt32(e));
                if (pc == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
 
                pc.AsignatarioID = db.AspNetUsers.Where(x => x.UserName == d).FirstOrDefault().Id;
                pc.ClienteID = Convert.ToInt32(a);
               // pc.EPedidoID = db.ESTADOS_PEDIDOS.Where(x => x.Nombre == "CREADO").Select(j => j.EPedidoID).FirstOrDefault();
                pc.Total = Convert.ToDecimal(c);
                pc.FechaCreacion = DateTime.Now;
                pc.FechaModificacion = DateTime.Now;

                if (ModelState.IsValid)
                {
                    
                       // SubCategory orig = db.SubCategory.Single(x => x.ID == subcategory.id);
                        if (TryUpdateModel<PEDIDOS_CLIENTES>(pc))
                        {
                            db.SaveChanges();
                        }
                    
                    

                }
                IQueryable<PEDIDOS_CLIENTES_DETALLES> pd = db.PEDIDOS_CLIENTES_DETALLES.Where(x => x.PedidoClienteID == pc.PedidoClienteID).Include(x => x.PRODUCTOS);
                
                if (pd == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    foreach (var p in _lstProductos)
                    {
                        PEDIDOS_CLIENTES_DETALLES pcd = pd.Single(x => x.ProductoID == p.productoID );

                        if (pcd != null)
                        {
                            pcd.ProductoID = p.productoID;
                            pcd.Cantidad = Convert.ToInt32(p.cantidad);
                            pcd.PrecioUnitario = p.precio;
                            db.SaveChanges();
                            

                        }
                        else
                        {
                            //SI NO LO ENCUENTRA CREA UN NUEVO DETALLE CON EL PRODUCTO
                            if (p != null)
                            {
                                PEDIDOS_CLIENTES_DETALLES _pcd = new PEDIDOS_CLIENTES_DETALLES();
                                _pcd.Cantidad = Convert.ToInt32(p.cantidad);
                                PRODUCTOS producto = db.PRODUCTOS.Where(x => x.ProductoID == p.productoID).FirstOrDefault();
                                if (p != null)
                                {
                                    _pcd.ProductoID = producto.ProductoID;
                                    _pcd.Detalle = producto.Detalle;
                                    _pcd.PrecioUnitario = p.precio;
                                    _pcd.PedidoClienteID = pc.PedidoClienteID;
                                    db.PEDIDOS_CLIENTES_DETALLES.Add(_pcd);
                                    db.SaveChanges();
                                }
                                else
                                    throw new Exception();

                            }
                            else
                                throw new Exception();
                        }

                    }

            }
            catch (Exception)
            {

                throw;
            }


            return RedirectToAction("Edit", new { id = Convert.ToInt32(e) });
        }

        // GET: /PedidoCliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            var pedidos_clientes = db.PEDIDOS_CLIENTES.Find(id);

            if (pedidos_clientes == null)
            {
                return HttpNotFound();
            }

            ModelViewEditFactura cf = new ModelViewEditFactura();

            cf.cliente = db.CLIENTES.Find(pedidos_clientes.ClienteID);
           
            cf.fyp = new ModelViewFacturacionYPago();
            
            var arrayPago = pedidos_clientes.TIPO_PAGOS.Select(x => x.TPagoID).ToArray();
            
            cf.fyp.tiposFacturacion = new SelectList(db.TIPO_FACTURACIONES, "TFacturacionID", "Descripcion", pedidos_clientes.TFacturacionID);

            cf.fyp.tiposPago = new SelectList(db.TIPO_PAGOS, "TPagoID", "Descripcion", arrayPago);

            cf.usuarios = new SelectList(db.AspNetUsers, "UserName", "UserName", db.AspNetUsers.Find(pedidos_clientes.AsignatarioID).UserName);
                   
            cf.productos = ObtenerProductosPorPedido(id);
          
            ViewBag.PedidoID = pedidos_clientes.PedidoClienteID;
         

            return View("Edit_ne", cf);
        }

        // POST: /PedidoCliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PedidoClienteID,EPedidoID,ClienteID,AsignatarioID,Total")] PEDIDOS_CLIENTES pedidos_clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos_clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email", pedidos_clientes.AsignatarioID);
            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre", pedidos_clientes.ClienteID);
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre", pedidos_clientes.EPedidoID);
            return View(pedidos_clientes);
        }

        // GET: /PedidoCliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDOS_CLIENTES pedidos_clientes = db.PEDIDOS_CLIENTES.Find(id);
            if (pedidos_clientes == null)
            {
                return HttpNotFound();
            }
            return View(pedidos_clientes);
        }

        // POST: /PedidoCliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PEDIDOS_CLIENTES pedidos_clientes = db.PEDIDOS_CLIENTES.Find(id);
            db.PEDIDOS_CLIENTES.Remove(pedidos_clientes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CrearNuevoPedido()
        {
            ViewBag.Usuarios = new SelectList(db.AspNetUsers, "UserName", "UserName", db.AspNetUsers.FirstOrDefault().UserName);
            
            //ModelViewCrearFactura pYf = new ModelViewCrearFactura();

            ModelViewEditFactura nuevoPedidoCliente = new ModelViewEditFactura();
            
            nuevoPedidoCliente.cliente = new CLIENTES();

            nuevoPedidoCliente.fyp = new ModelViewFacturacionYPago();

            nuevoPedidoCliente.fyp.tiposFacturacion = new SelectList(db.TIPO_FACTURACIONES, "TFacturacionID", "Descripcion");

            nuevoPedidoCliente.fyp.tiposPago = new SelectList(db.TIPO_PAGOS, "TPagoID", "Descripcion");

            nuevoPedidoCliente.usuarios = new SelectList(db.AspNetUsers, "UserName", "UserName");
            ///pYf.tiposFacturacion = db.TIPO_FACTURACIONES.Select(i => new SelectListItem() { Text = i.Descripcion, Value = SqlFunctions.StringConvert((double)i.TFacturacionID) });


            return View(nuevoPedidoCliente);
        }

        [HttpPost]
        public PartialViewResult BuscarNombreCliente(string keyword)
        {
            var dat = db.CLIENTES.Where(f => f.Nombre.Contains(keyword)).FirstOrDefault();


            return PartialView("_PartialDetalleCliente", dat);
           // return Json(new { data = clie }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public PartialViewResult BuscarProducto(string keyword)
        {
            var dat = db.PRODUCTOS.Where(f => f.Descripcion.Contains(keyword)).FirstOrDefault();

            if (dat != null)
            {
                CATEGORIAS cat = db.CATEGORIAS.Find(dat.CategoriaID);
                TIPO_IVA tIva = db.TIPO_IVA.Find(dat.TIvaID);
                ViewBag.Categoria = cat.Descripcion;
                ViewBag.Iva = tIva.Porcentaje;
            }

            return PartialView("_PartialDetalleProducto", dat);
            // return Json(new { data = clie }, JsonRequestBehavior.DenyGet);
        }

        
        [HttpPost]
        public PartialViewResult AgregarProductoGrid(IList<Models.ProductosJqueryViewModel> model)
        {
            //List<PRODUCTOS> _listProduct = new List<PRODUCTOS>();
            //PRODUCTOS prod;
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(keyword); 
            List<Models.ProductosJqueryViewModel> _nuevalist = new List<Models.ProductosJqueryViewModel>();

            foreach (var item in model)
	        {
               

                int id = Convert.ToInt32(item.productoID);
                var i = db.PRODUCTOS.Where(x => x.ProductoID == id).FirstOrDefault();
                //var can = db.PRODUCTOS.Count(x => x.ProductoID == id);
                //cantidad c = new cantidad();
                //c.cant = can;
                //List<cantidad> cantidad = new List<cantidad>();
                //cantidad.Add(c);

                //ViewBag.AsignatarioID = new SelectList(model, "Id", "Email", prod.ProductoID);
                if (i != null)
                {
                    Models.ProductosJqueryViewModel prod = new Models.ProductosJqueryViewModel();
                    prod.productoID = i.ProductoID;
                    prod.descripcion = i.Descripcion;
                    prod.cantidad = item.cantidad;
                    prod.precio = item.precio;
                    prod.subTotal = item.subTotal;
                    _nuevalist.Add(prod);
                }
                
	        }
            
           // ViewBag.Cantidad = new SelectList(model, "ProductoID", "Cantidad" );
           // _lstProd.Add(dat.FirstOrDefault()); 

            return PartialView("_PartialGridProductos", _nuevalist);
            // return Json(new { data = clie }, JsonRequestBehavior.DenyGet);
        }


        private List<ProductosJqueryViewModel> ObtenerProductosPorPedido(int? PedidoID ){


            

            List<ProductosJqueryViewModel> _lista = new List<ProductosJqueryViewModel>();

            if (PedidoID == null)
                return _lista;

            var productos = db.PEDIDOS_CLIENTES.Find(PedidoID);

            foreach (var item in productos.PEDIDOS_CLIENTES_DETALLES)
            {
                ProductosJqueryViewModel producto = new ProductosJqueryViewModel();
                producto.cantidad = item.Cantidad.ToString();
                producto.descripcion = item.Detalle;
                producto.precio = item.PrecioUnitario;
                producto.productoID = item.ProductoID;
                producto.subTotal = item.PrecioUnitario * item.Cantidad;

                _lista.Add(producto);

            }

            return _lista;



        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
