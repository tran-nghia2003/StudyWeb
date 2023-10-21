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
    public class TypeAccountsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: TypeAccounts
        public ActionResult Index()
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            return View(db.TypeAccount.ToList());
        }

        // GET: TypeAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAccount typeAccount = db.TypeAccount.Find(id);
            if (typeAccount == null)
            {
                return HttpNotFound();
            }
            return View(typeAccount);
        }

        // GET: TypeAccounts/Create
        public ActionResult Create()
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            return View();
        }

        // POST: TypeAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,typeName,display")] TypeAccount typeAccount)
        {
            if (ModelState.IsValid)
            {
                db.TypeAccount.Add(typeAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeAccount);
        }

        // GET: TypeAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAccount typeAccount = db.TypeAccount.Find(id);
            if (typeAccount == null)
            {
                return HttpNotFound();
            }
            return View(typeAccount);
        }

        // POST: TypeAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,typeName,display")] TypeAccount typeAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeAccount);
        }

        // GET: TypeAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAccount typeAccount = db.TypeAccount.Find(id);
            if (typeAccount == null)
            {
                return HttpNotFound();
            }
            return View(typeAccount);
        }

        // POST: TypeAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeAccount typeAccount = db.TypeAccount.Find(id);
            db.TypeAccount.Remove(typeAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
