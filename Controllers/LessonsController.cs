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
    public class LessonsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Lessons
 

        

        public ActionResult ListLesson(int? id)
        {
            List<Lesson> lessons = db.Lesson.Where(s => s.idChapter == id).ToList();

            // Tạo SelectList
            SelectList ListLesson = new SelectList(db.Lesson, "id", "contentLesson","linkLesson");

            // Set vào ViewBag
            ViewBag.LessonList = ListLesson;


            //ViewBag.idClass = new SelectList(db.Classs, "id", "className",idClass);
            //var subjects = db.Subjects.Where(s => s.idClass == idClass);
            return View(lessons.ToList());
        }
        
        public ActionResult Lesson(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = db.Lesson.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        

    }
}
