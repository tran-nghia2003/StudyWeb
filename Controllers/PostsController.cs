﻿using System;
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
    public class PostsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Posts
 
        public ActionResult ListPosts()
        {
            var posts = db.Posts.Include(p => p.Account).OrderByDescending(p => p.id);
            return View(posts.ToList());
        }

    }
}
