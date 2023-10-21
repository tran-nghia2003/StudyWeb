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
    public class DocumentsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Documents



        public ActionResult Document(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }
        public ActionResult ListDocument(int? id)
        {
            List<Document> documents = db.Document.Where(s => s.idSubjects == id).ToList();
            SelectList ListLesson = new SelectList(db.Lesson, "id", "contentLesson", "linkLesson");
            ViewBag.DocumentList = ListLesson;
            return View(documents.ToList());
        }

    }
}
