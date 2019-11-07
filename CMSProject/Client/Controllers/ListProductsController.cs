using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSProject.Models;

namespace CMSProject.Client.Controllers
{
    public class ListProductsController : Controller
    {
        private CMSEntities db = new CMSEntities();
        // GET: ListProducts
        public ActionResult Index()
        {
            var lstPro = db.Products.ToList();
            return View(lstPro);
        }
    }
}