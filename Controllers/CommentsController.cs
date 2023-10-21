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
    public class CommentsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Comments
        public ActionResult ListComments(int? id )
        {
            TempData["idPosts"] = id;
            List<Comment> comments = db.Comment.Where(s => s.idPosts == id).ToList();
            // Set vào ViewBag
            Session["idPosts"] = id;
            ViewBag.idUser = new SelectList(db.Account, "id", "fullname");
            ViewBag.idPosts = new SelectList(db.Posts, "id", "contentPosts");
            return View(comments);
        }

    }
}
