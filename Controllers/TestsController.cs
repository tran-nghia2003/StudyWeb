using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudyWeb.Models;

namespace StudyWeb.Controllers
{
    public class TestsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Tests

        public ActionResult ListTest(int? id)
        {
            List<Test> test = db.Test.Where(s => s.idSubjects == id).ToList();
            // Tạo SelectList
            SelectList listTest = new SelectList(db.Score, "idUser", "score1");
            // Set vào ViewBag
            ViewBag.TestList = listTest;
            return View(test.ToList());
        }
        public ActionResult Test(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Test.Find(id);
            if (test == null)
            {
                
                return HttpNotFound();
            }
            Session["timeTest"] = db.Test.Where(s => s.id == id).ToList().FirstOrDefault().timeTest;
            return View(test);
        }

    }
}
