using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSProject.Models;

namespace CMSProject.Controllers
{
    public class ProductsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Products
       
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Brand,Size,Description,InputPrice,Price,ProductCode,Quantity,Status,Height,Material,ProductImage,ImageUpload,SupplierID,Color,CategoryID,DateCreated")] Product product, HttpPostedFileBase file, FormCollection formValues)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Products.Add(product);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            //ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", product.SupplierID);
            //return View(product);

            //Modified code
            var temp = formValues["editor"];
            byte[] bytes;
            product.DateCreated = Convert.ToDateTime(string.Format("Item Created on {0:MM/dd/yyyy}", DateTime.Today));

            CMSEntities entities = new CMSEntities();
            try
            {
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }
               
                entities.Products.Add(new Product
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductCode = product.ProductCode,
                    Brand = product.Brand,
                    Size = product.Size,
                    Description = temp,
                    InputPrice=product.InputPrice,
                    Price = product.Price,
                    Supplier=product.Supplier,
                    Color = product.Color,
                    Quantity = product.Quantity,
                    Status = product.Status,
                    Height = product.Height,       
                    DateCreated=product.DateCreated,
                    Material = product.Material,
                    ImageUpload = bytes
               

            });
                entities.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    // extract only the fielname
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    product.ImageUpload = new byte[file.ContentLength];
                    file.InputStream.Read(product.ImageUpload, 0, file.ContentLength);
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                    file.SaveAs(path);
                }
                // Converting to bytes.               
                product.ImageUpload = new byte[file.ContentLength];
                file.InputStream.Read(product.ImageUpload, 0, file.ContentLength);
                if (ModelState.IsValid)
                {
                    
                    //db.Entry(product).State = EntityState.Modified;
                    //Original add product code
                    //db.Products.Add(product);
                    //await db.SaveChangesAsync();
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", product.SupplierID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", product.SupplierID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Brand,Size,Description,InputPrice,Price,ProductCode,Quantity,Status,Height,Material,ProductImage,ImageUpload,SupplierID,Color,CategoryID,DateCreated")] Product product, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(product).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //modifiy code 
            byte[] bytes;
            try
            {
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }

                //orginal code, revert in case error
                if (ModelState.IsValid)
                {
                    //modify code , delete when error

                    //end modify 
                    if (file != null)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Content/Uploads/") + file.FileName);
                        product.ImageUpload = bytes;
                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", product.SupplierID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
