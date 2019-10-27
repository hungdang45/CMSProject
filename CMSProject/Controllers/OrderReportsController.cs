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
    public class OrderReportsController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: OrderReports
        public ActionResult Index()
        {
            return View(db.OrderReports.ToList());
        }

        // GET: OrderReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReport orderReport = db.OrderReports.Find(id);
            if (orderReport == null)
            {
                return HttpNotFound();
            }
            return View(orderReport);
        }

        // GET: OrderReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderReportID,CustomerID,CreatedDay,Interest")] OrderReport orderReport)
        {
            if (ModelState.IsValid)
            {
                db.OrderReports.Add(orderReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderReport);
        }

        // GET: OrderReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReport orderReport = db.OrderReports.Find(id);
            if (orderReport == null)
            {
                return HttpNotFound();
            }
            return View(orderReport);
        }

        // POST: OrderReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderReportID,CustomerID,CreatedDay,Interest")] OrderReport orderReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderReport);
        }

        // GET: OrderReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReport orderReport = db.OrderReports.Find(id);
            if (orderReport == null)
            {
                return HttpNotFound();
            }
            return View(orderReport);
        }

        // POST: OrderReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderReport orderReport = db.OrderReports.Find(id);
            db.OrderReports.Remove(orderReport);
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
