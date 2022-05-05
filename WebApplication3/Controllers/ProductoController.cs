using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductoController : Controller
    {
        private CREntities1 db = new CREntities1();

        // GET: /Producto/
        public ActionResult Index()
        {
            var productos = db.PRODUCTOS.Include(p => p.CATEGORIAS).Include(p => p.PROVEEDORES).Include(p => p.TIPO_IVA);

            
            return View(productos.ToList());
        }

        // GET: /Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTOS productos = db.PRODUCTOS.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: /Producto/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.CATEGORIAS, "CategoriaID", "Descripcion");
            ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "Nombre");
            ViewBag.TIvaID = new SelectList(db.TIPO_IVA, "TIvaID", "Porcentaje");
            return View();
        }

        // POST: /Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection col)
        {
            var a = Request.Form["hddProducto"];
            var b = Request.Form["hddImagen"];


            try
            {


                JavaScriptSerializer js = new JavaScriptSerializer();
                PRODUCTOS prod = js.Deserialize<PRODUCTOS>(a);

              //  HttpWebRequest req = (HttpWebRequest)(System.Net.HttpWebRequest.Create("~" + b));
                prod.ImagenProducto = System.IO.File.ReadAllBytes(HttpContext.Server.MapPath("~" + b));
                db.PRODUCTOS.Add(prod);
                db.SaveChanges();
               // HttpWebResponse resp = (HttpWebResponse)req.GetResponse();  

                //Image img = Image.FromStream(resp.GetResponseStream());

                //resp.Close();
                
                //using (var ms = new MemoryStream())
                //{
                //    img.Save(ms, img.RawFormat);
                //    prod.ImagenProducto = ms.ToArray();
                //}


            }
            catch (Exception)
            {
                
                throw;
            }
            


            //ViewBag.CategoriaID = new SelectList(db.CATEGORIAS, "CategoriaID", "Descripcion", productos.CategoriaID);
            //ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "ListaPrecios", productos.ProveedorID);
            //ViewBag.TIvaID = new SelectList(db.TIPO_IVA, "TIvaID", "TIvaID", productos.TIvaID);
            return View();
        }

        // GET: /Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTOS productos = db.PRODUCTOS.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.CATEGORIAS, "CategoriaID", "Descripcion", productos.CategoriaID);
            ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "Nombre", productos.ProveedorID);
            ViewBag.TIvaID = new SelectList(db.TIPO_IVA, "TIvaID", "Porcentaje", productos.TIvaID);


            Image img = ArrayToImage(productos.ImagenProducto);
            string path = "~/images/" + productos.ProductoID + ".jpg";
            img.Save(HttpContext.Server.MapPath(path));
            ViewBag.urlImagen = path;

            return View(productos);
        }

        // POST: /Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection col)
        {
            var a = Request.Form["hddProducto"];
            var b = Request.Form["hddImagen"];


            try
            {


                JavaScriptSerializer js = new JavaScriptSerializer();
                PRODUCTOS prod = js.Deserialize<PRODUCTOS>(a);

                //  HttpWebRequest req = (HttpWebRequest)(System.Net.HttpWebRequest.Create("~" + b));
               // prod.ImagenProducto = System.IO.File.ReadAllBytes(HttpContext.Server.MapPath("~" + b));
                //PRODUCTOS p = db.PRODUCTOS.Find(prod.ProductoID);
                prod.ImagenProducto = System.IO.File.ReadAllBytes(HttpContext.Server.MapPath("~" + b));

                if (ModelState.IsValid)
                {
                    db.Entry(prod).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit",prod.ProductoID);
                }

                //db.PRODUCTOS.Add(prod);
                //db.SaveChanges();
                // HttpWebResponse resp = (HttpWebResponse)req.GetResponse();  

                //Image img = Image.FromStream(resp.GetResponseStream());

                //resp.Close();

                //using (var ms = new MemoryStream())
                //{
                //    img.Save(ms, img.RawFormat);
                //    prod.ImagenProducto = ms.ToArray();
                //}


            }
            catch (Exception)
            {

                throw;
            }



            //ViewBag.CategoriaID = new SelectList(db.CATEGORIAS, "CategoriaID", "Descripcion", productos.CategoriaID);
            //ViewBag.ProveedorID = new SelectList(db.PROVEEDORES, "ProveedorID", "ListaPrecios", productos.ProveedorID);
            //ViewBag.TIvaID = new SelectList(db.TIPO_IVA, "TIvaID", "TIvaID", productos.TIvaID);
            return View();
           


        }

        // GET: /Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTOS productos = db.PRODUCTOS.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: /Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTOS productos = db.PRODUCTOS.Find(id);
            db.PRODUCTOS.Remove(productos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult FileUpload(HttpPostedFileBase file)
        {
            string path = string.Empty;
            string pic = string.Empty;
            Image imagen = null; 
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                path = "~/images/" + DateTime.Now.ToString("ddMMyyyy-mm-ss") + pic.Substring(pic.Length-4,4);
                // file is uploaded
                //file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    MemoryStream nms = new MemoryStream(array);
                    imagen = Image.FromStream(nms);
                    imagen = ResizeImage(imagen, 318, 275);

                    imagen.Save(HttpContext.Server.MapPath(path));

                }

            }

            

            // after successfully uploading redirect the user
            return PartialView("PartialImagePreview", path);
        }

        public PartialViewResult VerImagen(string keyword)
        {
            string path = string.Empty;
            string pic = string.Empty;
            Image imagen = null;
            if (keyword != null)
            {
                PRODUCTOS producto = db.PRODUCTOS.Find(Convert.ToInt32(keyword));
               // imagen = new Image();
                //pic = System.IO.Path.GetFileName(file.FileName);
                //path = "~/images/" + DateTime.Now.ToString("ddMMyyyy-mm-ss") + pic.Substring(pic.Length - 4, 4);
                //// file is uploaded
                ////file.SaveAs(path);

                //// save the image path path to the database or you can send image 
                //// directly to database
                //// in-case if you want to store byte[] ie. for DB
                if(producto != null)
                    using (MemoryStream ms = new MemoryStream())
                    {
                        path = "~/images/" + keyword + ".jpg";
                        //file.InputStream.CopyTo(ms);
                        byte[] array = producto.ImagenProducto;
                        MemoryStream nms = new MemoryStream(array);
                        imagen = Image.FromStream(nms);
                        //imagen = ResizeImage(imagen, 318, 275);

                        imagen.Save(HttpContext.Server.MapPath(path));

                    }

            }



            // after successfully uploading redirect the user
            return PartialView("PartialImagePreview", path);
        }

        private Image ArrayToImage(byte[] array) {
            Image img;

            using (MemoryStream ms = new MemoryStream())
            {
                //path = "~/images/" + keyword + ".jpg";
                //file.InputStream.CopyTo(ms);
                MemoryStream nms = new MemoryStream(array);
                img = Image.FromStream(nms);
                //imagen = ResizeImage(imagen, 318, 275);

               // imagen.Save(HttpContext.Server.MapPath(path));

            }

            return img;
        
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
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
