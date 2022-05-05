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
    public class PedidoProveedorController : Controller
    {
        private CREntities1 db = new CREntities1();

        // GET: /PedidoProveedor/
        public ActionResult Index()
        {
            var pedidos_proveedores = db.PEDIDOS_PROVEEDORES.Include(p => p.AspNetUsers).Include(p => p.ESTADOS_PEDIDOS).Include(p => p.PROVEEDORES);
            return View(pedidos_proveedores.ToList());
        }


        // GET: /PedidoProveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDOS_PROVEEDORES pedidos_proveedores = db.PEDIDOS_PROVEEDORES.Find(id);
            if (pedidos_proveedores == null)
            {
                return HttpNotFound();
            }
            return View(pedidos_proveedores);
        }

        // GET: /PedidoProveedor/Create
        public ActionResult Create()
        {
            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre");
            ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "ListaPrecios");
            return View();
        }

        // POST: /PedidoProveedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PedidoProveedorID,EPedidoID,ProveedorID,AsignatarioID,Total")] PEDIDOS_PROVEEDORES pedidos_proveedores)
        {
            if (ModelState.IsValid)
            {
                db.PEDIDOS_PROVEEDORES.Add(pedidos_proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email", pedidos_proveedores.AsignatarioID);
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre", pedidos_proveedores.EPedidoID);
            ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "ListaPrecios", pedidos_proveedores.ProveedorID);
            return View(pedidos_proveedores);
        }

        // GET: /PedidoProveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDOS_PROVEEDORES pedidos_proveedores = db.PEDIDOS_PROVEEDORES.Find(id);
            if (pedidos_proveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email", pedidos_proveedores.AsignatarioID);
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre", pedidos_proveedores.EPedidoID);
            ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "ListaPrecios", pedidos_proveedores.ProveedorID);
            return View(pedidos_proveedores);
        }

        // POST: /PedidoProveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PedidoProveedorID,EPedidoID,ProveedorID,AsignatarioID,Total")] PEDIDOS_PROVEEDORES pedidos_proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos_proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsignatarioID = new SelectList(db.AspNetUsers, "Id", "Email", pedidos_proveedores.AsignatarioID);
            ViewBag.EPedidoID = new SelectList(db.ESTADOS_PEDIDOS, "EPedidoID", "Nombre", pedidos_proveedores.EPedidoID);
            ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "ListaPrecios", pedidos_proveedores.ProveedorID);
            return View(pedidos_proveedores);
        }

        // GET: /PedidoProveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDOS_PROVEEDORES pedidos_proveedores = db.PEDIDOS_PROVEEDORES.Find(id);
            if (pedidos_proveedores == null)
            {
                return HttpNotFound();
            }
            return View(pedidos_proveedores);
        }

        // POST: /PedidoProveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PEDIDOS_PROVEEDORES pedidos_proveedores = db.PEDIDOS_PROVEEDORES.Find(id);
            db.PEDIDOS_PROVEEDORES.Remove(pedidos_proveedores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CrearNuevoPedido()
        {


            return View();
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
