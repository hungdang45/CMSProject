using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CMSProject.Models;



namespace CMSProject.Controllers
{
    public class CustomersController : Controller
    {
        private CMSEntities db = new CMSEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,Address,Phone,Email,Password,City,Country")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerName,Address,Phone,Email,Password,City,Country")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //Registration POST action 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] Customer customer)
        {
            bool Status = false;
            string message = "";
           
            //
            // Model Validation 
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(customer.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(customer);
                }
                #endregion

                #region Generate Activation Code 
                customer.ActivationCode = Guid.NewGuid();
                #endregion

               
                customer.Password = Crypto.Hash(customer.Password);
              
               
                customer.IsEmailVerified = false;

                #region Save to Database
                using (CMSEntities db = new CMSEntities())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    //Send Email to User
                    //SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    //message = "Registration successfully done. Account activation link " +
                    //    " has been sent to your email id:" + user.EmailID;
                    //Status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(customer);
        }
        //Verify Account  

        //[HttpGet]
        //public ActionResult VerifyAccount(string id)
        //{
        //    bool Status = false;
        //    using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    {
        //        dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
        //                                                        // Confirm password does not match issue on save changes
        //        var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
        //        if (v != null)
        //        {
        //            v.IsEmailVerified = true;
        //            dc.SaveChanges();
        //            Status = true;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Invalid Request";
        //        }
        //    }
        //    ViewBag.Status = Status;
        //    return View();
        //}

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (CMSEntities db = new CMSEntities())
            {
                var v = db.Customers.Where(a => a.Email == login.Email).FirstOrDefault();
                if (v != null)
                {
                    if (v.IsEmailVerified ??false)
                        
                        {
                        ViewBag.Message = "Please verify your email first";
                        return View();
                    }

                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        // 525600 min = 1 year
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Customers");
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (CMSEntities dc = new CMSEntities())
            {
                var v = dc.Customers.Where(a => a.CustomerName == emailID).FirstOrDefault();
                return v != null;
            }
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
