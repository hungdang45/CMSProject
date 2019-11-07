using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSProject.Models;

namespace CMSProject.Controllers
{
    public class OrderController : Controller
    {
        private CMSEntities db = new CMSEntities();
        List<Carts> lstCarts = new List<Carts>();
        // GET: Order
        public ActionResult Index()
        {
            if (Session["AdminName"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            return View(db.Orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderName,CustomerID,CustomerName,CustomerEmail,Gender,Phone,ShipAddress,OrderCreate,CreateDate,Shipper,ShipDate,Receiver,PayDate,Total,Note,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderName = 1;
                order.CreateDate = DateTime.Now;
                order.Status = 1;
                order.OrderCreate = Session["AdminName"].ToString();
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderName,CustomerID,CustomerName,CustomerEmail,Gender,Phone,ShipAddress,OrderCreate,CreateDate,Shipper,ShipDate,Receiver,PayDate,Total,Note,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public ActionResult CreateProduct(int? id)
        {
            Order order = db.Orders.Find(id);
            TempData["IdOrder"] = id;
            return View(order);
        }
        [HttpPost]
        public ActionResult CreateProduct(int? id, int? ordId)
        {
            if (Session["Carts"] == null)
            {
                lstCarts = new List<Carts>();
            }
            else
            {
                lstCarts = Session["Carts"] as List<Carts>;
            }
            Product pro = db.Products.Where(n => n.ProductID == id).FirstOrDefault();
            Carts c = new Carts();
            int IdOrd = ordId.Value;
            c.OrderID = IdOrd;
            c.ProductID = id.Value;
            c.ProductName = pro.ProductName;
            c.Quantity = 1;
            c.UnitPrice = Convert.ToDecimal(pro.Price);
            c.Total = c.Quantity * c.UnitPrice;
            lstCarts.Add(c);
            Session["Carts"] = lstCarts;
            return RedirectToAction("/CreateProduct/"+IdOrd);
        }

        [HttpPost]
        public ActionResult EditQuantity(int id,int Quantitied, int? IdOr)
        {
            if (Session["Carts"] == null)
            {
                lstCarts = new List<Carts>();
            }
            else
            {
                lstCarts = Session["Carts"] as List<Carts>;
            }
            if (lstCarts != null)
            {
                lstCarts.Where(n => n.ProductID == id).FirstOrDefault().Quantity = Quantitied;
                lstCarts.Where(n => n.ProductID == id).FirstOrDefault().Total = lstCarts.Where(n => n.ProductID == id).FirstOrDefault().UnitPrice * Quantitied;
            }
            else
            {
                return null;
            }
            int idOpr = IdOr.Value;
            Session["Carts"] = lstCarts;
            return RedirectToAction("/CreateProduct/"+idOpr);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int id,int? IdOr)
        {
            if (Session["Carts"] == null)
            {
                lstCarts = new List<Carts>();
            }
            else
            {
                lstCarts = Session["Carts"] as List<Carts>;
            }
            if (lstCarts != null)
            {
                Carts pro = lstCarts.Where(n => n.ProductID == id).FirstOrDefault();
                lstCarts.Remove(pro);
            }
            int idOpr = IdOr.Value;
            return RedirectToAction("/CreateProduct/" + idOpr);
        }
        public ActionResult listProduct()
        {
            ViewBag.IdO = TempData["IdOrder"];
            var ProductData = db.Products.ToList();
            return View(ProductData);
        }

        public ActionResult lstOrderProduct()
        {
            if (Session["Carts"] == null)
            {
                lstCarts = new List<Carts>();
            }
            else
            {
                lstCarts = Session["Carts"] as List<Carts>;
            }
            int idO = Int32.Parse(TempData["IdOrder"].ToString());
            var dataLstPro = lstCarts.Where(n=>n.OrderID==idO).ToList();
            return View(dataLstPro);
        }

        public ActionResult saveToOrderDetails(int id)
        {
            if (Session["Carts"] != null)
            {
                lstCarts = Session["Carts"] as List<Carts>;
            }
            var lstCartting = lstCarts.Where(n => n.OrderID == id).ToList();
            foreach(var item in lstCartting)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = item.OrderID;
                orderDetail.ProductID = item.ProductID;
                orderDetail.ProductName = item.ProductName;
                orderDetail.Quantity = item.Quantity;
                orderDetail.UnitPrice = item.UnitPrice;
                orderDetail.Total = item.Total;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
