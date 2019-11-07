//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using CMSProject.Configuration;
//using CMSProject.Models;
//using RestSharp;
//using System.Configuration;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using CMSProject.Configuration;
//using System;

//namespace CMSProject.Controllers
//{
//    public class CheckoutController : Controller
//    {
//        CMSEntities db = new CMSEntities();
//        AppConfigurations appConfig = new AppConfigurations();

//        //public List<String> CreditCardTypes { get { return appConfig.CreditCardType; } }

//        //
//        // GET: /Checkout/AddressAndPayment
//        public ActionResult AddressAndPayment()
//        {
//            //ViewBag.CreditCardTypes = CreditCardTypes;
//            var previousOrder = db.Orders.FirstOrDefault(x => x.OrderName == User.Identity.Name);

//            if (previousOrder != null)
//                return View(previousOrder);
//            else
//                return View();
//        }

//        //
//        // POST: /Checkout/AddressAndPayment
//        [HttpPost]
//        public async Task<ActionResult> AddressAndPayment(FormCollection values)
//        {
//            //ViewBag.CreditCardTypes = CreditCardTypes;
//            string result = values[9];

//            var order = new Order();
//            TryUpdateModel(order);
//            //order.CreditCard = result;

//            try
//            {
//                order.OrderName = User.Identity.Name;
//                order.Email = User.Identity.Name;
//                order.OrderDate = DateTime.Now;
//                var currentUserId = User.Identity.GetUserId();

//                if (order.SaveInfo && !order.Username.Equals("guest@guest.com"))
//                {

//                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
//                    var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
//                    var ctx = store.Context;
//                    var currentUser = manager.FindById(User.Identity.GetUserId());

//                    currentUser.UserName = order.Username;
//                    currentUser.Email = order.Email;
//                    currentUser.PhoneNumber = order.Phone;
                    

//                    //Save this back
//                    //http://stackoverflow.com/questions/20444022/updating-user-data-asp-net-identity
//                    //var result = await UserManager.UpdateAsync(currentUser);
//                    await ctx.SaveChangesAsync();

//                    await db.SaveChangesAsync();
//                }


//                //Save Order
//                db.Orders.Add(order);
//                await db.SaveChangesAsync();
//                //Process the order
//                var cart = ShoppingCart.GetCart(this.HttpContext);
//                order = cart.CreateOrder(order);



//                //CheckoutController.SendOrderMessage(order.Username, "New Order: " + order.OrderID, order.ToString(order), appConfig.OrderEmail);

//                return RedirectToAction("Complete",
//                    new { id = order.OrderID });

//            }
//            catch
//            {
//                //Invalid - redisplay with errors
//                return View(order);
//            }
//        }

//        //
//        // GET: /Checkout/Complete
//        public ActionResult Complete(int id)
//        {
//            // Validate customer owns this order
//            bool isValid = db.Orders.Any(
//                o => o.OrderID == id &&
//                o.Username == User.Identity.Name);

//            if (isValid)
//            {
//                return View(id);
//            }
//            else
//            {
//                return View("Error");
//            }
//        }

//        //private static RestResponse SendOrderMessage(String toName, String subject, String body, String destination)
//        //{
//        //    RestClient client = new RestClient();
//        //    //fix this we have this up top too
//        //    AppConfigurations appConfig = new AppConfigurations();
//        //    client.BaseUrl = "https://api.mailgun.net/v2";
//        //    client.Authenticator =
//        //           new HttpBasicAuthenticator("api",
//        //                                      appConfig.EmailApiKey);
//        //    RestRequest request = new RestRequest();
//        //    request.AddParameter("domain",
//        //                        appConfig.DomainForApiKey, ParameterType.UrlSegment);
//        //    request.Resource = "{domain}/messages";
//        //    request.AddParameter("from", appConfig.FromName + " <" + appConfig.FromEmail + ">");
//        //    request.AddParameter("to", toName + " <" + destination + ">");
//        //    request.AddParameter("subject", subject);
//        //    request.AddParameter("html", body);
//        //    request.Method = Method.POST;
//        //    IRestResponse executor = client.Execute(request);
//        //    return executor as RestResponse;
//        //}
//    }
//}