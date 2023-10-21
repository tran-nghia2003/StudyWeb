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
    public class QuestionsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Questions

        public ActionResult ListQuestion(int? id)
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            Session["idTest"] = id;
            List<Question> question = db.Question.Where(s => s.idTest == id).ToList();
            // Tạo SelectList
            SelectList listQuestion = new SelectList(db.Classs, "id", "className");
            // Set vào ViewBag
            ViewBag.TestList = listQuestion;
            this.Session["correctAnswer"] = question;
            return View(question.ToList());
        }

    }
}
