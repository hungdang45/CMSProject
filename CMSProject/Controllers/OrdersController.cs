//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using CMSProject.Models;

//namespace CMSProject.Controllers
//{
//    public class OrdersController : Controller
//    {
//        private CMSEntities db = new CMSEntities();

//        // GET: Orders
//        public ActionResult Index()
//        {
//            //var orders = db.Orders.Include(o => o.Customer).Include(o => o.OrderReport);
//            //return View(orders.ToList());
//            if (Session["ID"] == null)
//            {
//                return RedirectToAction("Login", "Accounts");
//            }
//            else
//            {
//                int checkRoles = Convert.ToInt32(Session["Roles"]);
//                if (checkRoles == 1)
//                {
//                    return View(db.Orders.ToList());
//                }
//                else
//                {
//                    return RedirectToAction("Error", "Home");
//                }
//            }
//        }

//        // GET: Orders/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Order order = db.Orders.Find(id);
//            if (order == null)
//            {
//                return HttpNotFound();
//            }
//            return View(order);
//        }

//        // GET: Orders/Create
//        public ActionResult Create()
//        {
//            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
//            ViewBag.OrderReportID = new SelectList(db.OrderReports, "OrderReportID", "CustomerID");
//            return View();
//        }

//        // POST: Orders/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "OrderID,CustomerID,OrderName,OrderDate,ShipAddress,ShippedDate,ShippingStatus,OrderReportID,Total")] Order order)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Orders.Add(order);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", order.CustomerID);
//            ViewBag.OrderReportID = new SelectList(db.OrderReports, "OrderReportID", "CustomerID", order.OrderReportID);
//            return View(order);
//        }

//        // GET: Orders/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Order order = db.Orders.Find(id);
//            if (order == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", order.CustomerID);
//            ViewBag.OrderReportID = new SelectList(db.OrderReports, "OrderReportID", "CustomerID", order.OrderReportID);
//            return View(order);
//        }

//        // POST: Orders/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,OrderName,OrderDate,ShipAddress,ShippedDate,ShippingStatus,OrderReportID,Total")] Order order)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(order).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", order.CustomerID);
//            ViewBag.OrderReportID = new SelectList(db.OrderReports, "OrderReportID", "CustomerID", order.OrderReportID);
//            return View(order);
//        }

//        // GET: Orders/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Order order = db.Orders.Find(id);
//            if (order == null)
//            {
//                return HttpNotFound();
//            }
//            return View(order);
//        }

//        // POST: Orders/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Order order = db.Orders.Find(id);
//            db.Orders.Remove(order);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
