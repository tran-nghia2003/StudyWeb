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
    public class VideosController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Videos

        public ActionResult ListVideo(int? id)
        {
            List<Video> video = db.Video.Where(v => v.idSubjects == id).ToList();
            return View(video.ToList());
        }





    }
}
