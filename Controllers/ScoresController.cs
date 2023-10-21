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
    public class ScoresController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Scores

        public ActionResult ScoreUser(int? id)
        {
            List<Score> scores = db.Score.Where(s => s.idUser == id).ToList();
            // Set vào ViewBag
            ViewBag.idUser = new SelectList(db.Account, "id", "fullname");
            return View(scores);
        }


    }
}
