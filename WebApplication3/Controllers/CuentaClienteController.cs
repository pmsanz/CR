using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CuentaClienteController : Controller
    {
        private CREntities1 db = new CREntities1();

       

        // GET: CuentaCliente
        public ActionResult Index()
        {
            var cUENTAS_CLIENTES = db.CUENTAS_CLIENTES.Include(c => c.CLIENTES);
            return View(cUENTAS_CLIENTES.ToList());
        }

        // GET: CuentaCliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUENTAS_CLIENTES cUENTAS_CLIENTES = db.CUENTAS_CLIENTES.Find(id);
            if (cUENTAS_CLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cUENTAS_CLIENTES);
        }

        // GET: CuentaCliente/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre");
            return View();
        }

        // POST: CuentaCliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CCuentaID,ClienteID,NumeroCuenta,Descripcion")] CUENTAS_CLIENTES cUENTAS_CLIENTES)
        {
            if (ModelState.IsValid)
            {
                db.CUENTAS_CLIENTES.Add(cUENTAS_CLIENTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre", cUENTAS_CLIENTES.ClienteID);
            return View(cUENTAS_CLIENTES);
        }

        // GET: CuentaCliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUENTAS_CLIENTES cUENTAS_CLIENTES = db.CUENTAS_CLIENTES.Find(id);
            if (cUENTAS_CLIENTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre", cUENTAS_CLIENTES.ClienteID);
            return View(cUENTAS_CLIENTES);
        }

        // POST: CuentaCliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CCuentaID,ClienteID,NumeroCuenta,Descripcion")] CUENTAS_CLIENTES cUENTAS_CLIENTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUENTAS_CLIENTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.CLIENTES, "ClienteID", "Nombre", cUENTAS_CLIENTES.ClienteID);
            return View(cUENTAS_CLIENTES);
        }

        // GET: CuentaCliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUENTAS_CLIENTES cUENTAS_CLIENTES = db.CUENTAS_CLIENTES.Find(id);
            if (cUENTAS_CLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cUENTAS_CLIENTES);
        }

        // POST: CuentaCliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUENTAS_CLIENTES cUENTAS_CLIENTES = db.CUENTAS_CLIENTES.Find(id);
            db.CUENTAS_CLIENTES.Remove(cUENTAS_CLIENTES);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult BuscarCuentas(string keyword)
        {
            List<CUENTAS_CLIENTES> cuentas = new List<CUENTAS_CLIENTES>();
            ModelViewMovimientoCuentaCliente cuentaCliente = new ModelViewMovimientoCuentaCliente(1);
            try
            {
                int id = Convert.ToInt32(keyword);
                cuentas = db.CUENTAS_CLIENTES.Where(x => x.ClienteID == id).ToList();

                if (cuentas != null)
                {

                   
                    cuentaCliente.cuentas = new SelectList(cuentas, "CCuentaID", "Descripcion");
                    cuentaCliente.fechaPago = DateTime.Now;

                }


            }
            catch (Exception)
            {
                
                throw;
            }

            return PartialView("_PartialViewDetalleMovimiento", cuentaCliente);
        }

        public string BuscarPedido(string keyword)
        {
            int id = Convert.ToInt32(keyword);

            PEDIDOS_CLIENTES pedido = db.PEDIDOS_CLIENTES.Find(id);

            if (pedido != null)
                return pedido.PedidoClienteID.ToString();
            else
                return "No existen resultados";
        }

        public ActionResult CrearNuevoMovimiento()
        {

            ModelViewMovimientoCuentaCliente mc = new ModelViewMovimientoCuentaCliente(1);

            mc.cliente = new CLIENTES();

            mc.cuentas = new SelectList(String.Empty);

            mc.fechaPago = DateTime.Now;

            mc.fyp = new ModelViewFacturacionYPago();

            mc.fyp.tiposFacturacion = new SelectList(db.TIPO_FACTURACIONES, "TFacturacionID", "Descripcion");

            mc.fyp.tiposPago = new SelectList(db.TIPO_PAGOS, "TPagoID", "Descripcion");

            return View("MovimientoCuentaCliente", mc);
        }

        public ActionResult NuevoMovimiento(FormCollection col)
        {
            //Datos cliente
            var cliente = Request.Form["hddClienteID"];

            //Tipo Pago 
            var contado = Convert.ToBoolean(Request.Form["chk1"].Split(',')[0]);
            var cheque = Convert.ToBoolean(Request.Form["chk2"].Split(',')[0]);
            var mercaderia = Convert.ToBoolean(Request.Form["chk3"].Split(',')[0]);

            var numeroCheque = Request.Form["txtNumeroCheque"];
            var importe =  Convert.ToDecimal(Request.Form["txtImporte"]);
            var detalleMercaderia = Request.Form["areaMercaderia"];
            var valorMercaderia = Convert.ToDecimal(Request.Form["txtValorMercaderia"]);
            var nombreUsuario = Request.Form["txtNombreUsuario"];
            var numeroCuenta = Request.Form["txtNumeroCuenta"];
            // CC o CA
            var cc = Convert.ToBoolean(Request.Form["chkCC"].Split(',')[0]);
            var ca = Convert.ToBoolean(Request.Form["chkCA"].Split(',')[0]);
            
           // var ca = Request.Form["chkCA"];
            
            //Fecha Pago
            var fechaPago = Convert.ToDateTime(Request.Form["fechaPago"]);
            var tipoPago = db.TIPO_PAGOS.Find(Convert.ToInt32(Request.Form["Tipo"]));
            var cuentaCliente = new CUENTAS_CLIENTES();
            if(Request.Form["Cuenta"] == null)
                cuentaCliente = db.CUENTAS_CLIENTES.Find(cliente);
            cuentaCliente = db.CUENTAS_CLIENTES.Find(Convert.ToInt32(Request.Form["Cuenta"]));
            
            //Referencia
            var idReferencia = Request.Form["PedidoID"];
            var detalle = Request.Form["Detalle"];



            ModelViewMovimientoCuentaCliente mc = new ModelViewMovimientoCuentaCliente(1);

            mc.cliente = new CLIENTES();

            mc.cuentas = new SelectList(String.Empty);

            mc.fechaPago = DateTime.Now;

            mc.fyp = new ModelViewFacturacionYPago();

            mc.fyp.tiposFacturacion = new SelectList(db.TIPO_FACTURACIONES, "TFacturacionID", "Descripcion");

            mc.fyp.tiposPago = new SelectList(db.TIPO_PAGOS, "TPagoID", "Descripcion");

            return View("MovimientoCuentaCliente", mc);
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
