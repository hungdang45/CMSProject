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
//    public class CartsController : Controller
//    {
//        private CMSEntities db = new CMSEntities();

//        // GET: Carts
//        public ActionResult Index()
//        {
//            //var carts = db.Carts.Include(c => c.Product);
//            //return View(carts.ToList());

//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            // Set up our ViewModel
//            var viewModel = new ShoppingCartViewModel
//            {
//                CartItems = cart.GetCartItems(),
//                CartTotal = cart.GetTotal()
//            };
//            // Return the view
//            return View(viewModel);
//        }

//        [HttpPost]
//        public ActionResult AddToCart(int id)
//        {
//            // Retrieve the item from the database
//            var addedItem = db.Products
//                .Single(item => item.ProductID == id);

//            // Add it to the shopping cart
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            int count = cart.AddToCart(addedItem);

//            // Display the confirmation message
//            var results = new ShoppingCartRemoveViewModel
//            {
//                Message = Server.HtmlEncode(addedItem.ProductName) +
//                    " has been added to your shopping cart.",
//                CartTotal = cart.GetTotal(),
//                CartCount = cart.GetCount(),
//                ItemCount = count,
//                DeleteId = id
//            };
//            return Json(results);

//            // Go back to the main store page for more shopping
//            // return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public ActionResult RemoveFromCart(int id)
//        {
//            // Remove the item from the cart
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            // Get the name of the item to display confirmation

//            // Get the name of the album to display confirmation
//            string itemName = db.Products
//                .Single(item => item.ProductID == id).ProductName;

//            // Remove from cart
//            int itemCount = cart.RemoveFromCart(id);

//            // Display the confirmation message
//            var results = new ShoppingCartRemoveViewModel
//            {
//                Message = "One (1) " + Server.HtmlEncode(itemName) +
//                    " has been removed from your shopping cart.",
//                CartTotal = cart.GetTotal(),
//                CartCount = cart.GetCount(),
//                ItemCount = itemCount,
//                DeleteId = id
//            };
//            return Json(results);
//        }

//        public ActionResult CartSummary()
//        {
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            ViewData["CartCount"] = cart.GetCount();
//            return PartialView("CartSummary");
//        }

//        // GET: Carts/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Cart cart = db.Carts.Find(id);
//            if (cart == null)
//            {
//                return HttpNotFound();
//            }
//            return View(cart);
//        }

//        // GET: Carts/Create
//        public ActionResult Create()
//        {
//            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName");
//            return View();
//        }

//        // POST: Carts/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,CartId,ProductId,Count,DateCreated")] Cart cart)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Carts.Add(cart);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductId);
//            return View(cart);
//        }

//        // GET: Carts/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Cart cart = db.Carts.Find(id);
//            if (cart == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductId);
//            return View(cart);
//        }

//        // POST: Carts/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,CartId,ProductId,Count,DateCreated")] Cart cart)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(cart).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ProductId = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductId);
//            return View(cart);
//        }

//        // GET: Carts/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Cart cart = db.Carts.Find(id);
//            if (cart == null)
//            {
//                return HttpNotFound();
//            }
//            return View(cart);
//        }

//        // POST: Carts/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Cart cart = db.Carts.Find(id);
//            db.Carts.Remove(cart);
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
