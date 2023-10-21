using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lesson()
        {
            return View();
        }
        public ActionResult Posts()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}