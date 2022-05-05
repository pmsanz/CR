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
    public class ProveedorController : Controller
    {
        private CREntities1 db = new CREntities1();

        // GET: /Proveedor/
        public ActionResult Index()
        {
            var proveedores = db.PROVEEDORES.Include(p => p.PROVINCIAS);
            return View(proveedores.ToList());
        }

        // GET: /Proveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDORES proveedores = db.PROVEEDORES.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // GET: /Proveedor/Create
        public ActionResult Create()
        {
            ViewBag.ProvinciaID = new SelectList(db.PROVINCIAS, "ProvinciaID", "Nombre");
            return View();
        }

        // POST: /Proveedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProveedorID,ListaPrecios,Nombre,RazonSocial,CUIT_CUIL,Direccion1,Direccion2,ProvinciaID,Localidad,CodPostal,Latitud,Longitud,Zoom,Telefono1,Telefono2,Mail1,Mail2,Descuento,Comentarios,Rubro,NumeroCuenta,Banco,CBU,CA,CC,UsuarioMercadolibre")] PROVEEDORES proveedores)
        {
            if (ModelState.IsValid)
            {
                db.PROVEEDORES.Add(proveedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProvinciaID = new SelectList(db.PROVINCIAS, "ProvinciaID", "Nombre", proveedores.ProvinciaID);
            return View(proveedores);
        }

        // GET: /Proveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDORES proveedores = db.PROVEEDORES.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProvinciaID = new SelectList(db.PROVINCIAS, "ProvinciaID", "Nombre", proveedores.ProvinciaID);
            return View(proveedores);
        }

        // POST: /Proveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProveedorID,ListaPrecios,Nombre,RazonSocial,CUIT_CUIL,Direccion1,Direccion2,ProvinciaID,Localidad,CodPostal,Latitud,Longitud,Zoom,Telefono1,Telefono2,Mail1,Mail2,Descuento,Comentarios,Rubro,NumeroCuenta,Banco,CBU,CA,CC,UsuarioMercadolibre")] PROVEEDORES proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinciaID = new SelectList(db.PROVINCIAS, "ProvinciaID", "Nombre", proveedores.ProvinciaID);
            return View(proveedores);
        }

        // GET: /Proveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROVEEDORES proveedores = db.PROVEEDORES.Find(id);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }

        // POST: /Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROVEEDORES proveedores = db.PROVEEDORES.Find(id);
            db.PROVEEDORES.Remove(proveedores);
            db.SaveChanges();
            return RedirectToAction("Index");
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
