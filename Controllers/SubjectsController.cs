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
    public class SubjectsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Subjects

        public ActionResult ListSubjects(int? id)
        {
            List<Subjects> subjects = db.Subjects.Where(s => s.idClass == id).ToList();
            // Tạo SelectList
            SelectList listSubjects = new SelectList(db.Classs, "id", "className");
            // Set vào ViewBag
            ViewBag.SubjectList = listSubjects;
            return View(subjects.ToList());
        }
        public ActionResult ListSubjectsTest(int? id)
        {
            List<Subjects> subjects = db.Subjects.Where(s => s.idClass == id).ToList();
            // Tạo SelectList
            SelectList listSubjects = new SelectList(db.Classs, "id", "className");
            // Set vào ViewBag
            ViewBag.SubjectList = listSubjects;
            return View(subjects.ToList());
        }
        public ActionResult ListSubjectsVideo(int? id)
        {
            List<Subjects> subjects = db.Subjects.Where(s => s.idClass == id).ToList();
            // Tạo SelectList
            SelectList listSubjects = new SelectList(db.Classs, "id", "className");
            // Set vào ViewBag
            ViewBag.SubjectList = listSubjects;
            return View(subjects.ToList());
        }

    }
}
