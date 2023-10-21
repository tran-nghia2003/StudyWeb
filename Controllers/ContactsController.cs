using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using StudyWeb.Models;

namespace StudyWeb.Controllers
{
    public class ContactsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Contacts
        public ActionResult Contact()
        {
            return View();
        }



    }
}
