﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Về chúng tôi";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Thông tin liên lạc ";

            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
    }
}