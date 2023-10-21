using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudyWeb.Models;

namespace StudyWeb.Controllers
{
    public class ClasssesController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Classses
        public ActionResult ListClass()
        {
            return View(db.Classs.ToList());
        }

        public ActionResult ListClassTest()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            return View(db.Classs.ToList());
        }
        public ActionResult ListClassVideo()
        {
            return View(db.Classs.ToList());
        }
    }
}
