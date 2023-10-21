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
    public class ChaptersController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Chapters

        public ActionResult ListChapter(int? id)
        {
            List<Chapter> chapters = db.Chapter.Where(s => s.idSubjects == id).ToList();

            // Tạo SelectList
            SelectList ListChapter = new SelectList(db.Chapter, "id", "chapterName");

            // Set vào ViewBag
            ViewBag.ChapterList = ListChapter;


            //ViewBag.idClass = new SelectList(db.Classs, "id", "className",idClass);
            //var subjects = db.Subjects.Where(s => s.idClass == idClass);
            return View(chapters.ToList());
        }




    }
}
