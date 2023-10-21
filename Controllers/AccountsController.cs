using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using StudyWeb.Models;

namespace StudyWeb.Controllers
{
    public class AccountsController : Controller
    {
        private StudyWebEntities db = new StudyWebEntities();

        // GET: Accounts
        public ActionResult Index()
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            var account = db.Account.Include(a => a.TypeAccount);
            return View(account.ToList());
        }

        // GET: Accounts/Details/5
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
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            if (Session["accountAdmin"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            ViewBag.idType = new SelectList(db.TypeAccount, "id", "typeName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idType,fullname,imageUser,maill,phone,password,status,display")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idType = new SelectList(db.TypeAccount, "id", "typeName", account.idType);
            return View(account);
        }

        // GET: Accounts/Edit/5
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
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.idType = new SelectList(db.TypeAccount, "id", "typeName", account.idType);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idType,fullname,imageUser,maill,phone,password,status,display")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idType = new SelectList(db.TypeAccount, "id", "typeName", account.idType);
            return View(account);
        }

        // GET: Accounts/Delete/5
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
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Account.Find(id);
            db.Account.Remove(account);
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string maill, string password)
        {
            if (String.IsNullOrEmpty(maill))
            {
                ViewData["Error1"] = "Phải nhập tên đăng nhập!";
            }
            if (String.IsNullOrEmpty(password))
            {
                ViewData["Error2"] = "Phải nhập mật khẩu!";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var f_password = GetMD5(password);
                    var data = db.Account.Where(s => s.maill.Equals(maill) && s.password.Equals(f_password)).ToList();
                    if (data.Count > 0)
                    {
                        if (data.FirstOrDefault().status.Equals("Hoạt động"))
                        {
                            if (data.FirstOrDefault().idType.Equals(1))
                            {
                                Session["fullname"] = data.FirstOrDefault().fullname;

                                Session["mail"] = data.FirstOrDefault().maill;
                                Session["idUser"] = data.FirstOrDefault().id;
                                Session["imageUser"] = data.FirstOrDefault().imageUser;
                                Session["accountAdmin"] = data.FirstOrDefault().TypeAccount.id;
                                return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                Session["fullname"] = data.FirstOrDefault().fullname;
                                Session["imageUser"] = data.FirstOrDefault().imageUser;
                                Session["mail"] = data.FirstOrDefault().maill;
                                Session["idUser"] = data.FirstOrDefault().id;
                                Session["typeAccount"] = data.FirstOrDefault().idType;
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            ViewData["ErrorLock"] = "Tài khoản của bạn hiện đang bị khóa!";
                        }
                    }
                    else
                    {
                        ViewData["Error3"] = "Mail hoặc mật khẩu không chính xác";
                        return View();
                    }
                }
            }
            


            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                var check = db.Account.FirstOrDefault(s => s.maill == account.maill);
                if (check == null)
                {
                    account.imageUser = "user.png";
                    account.password = GetMD5(account.password);
                    account.idType = 3;
                    account.status = "Hoạt động";
                    account.display = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Account.Add(account);
                    db.SaveChanges();
                    ViewBag.success = "Đăng ký thành công!";
                    return RedirectToAction("Login", "Accounts");
                }
                else
                {
                    ViewData["ErrorMaill"] = "Mail đã tồn tại!";
                    return View();
                }
            }
            return View();
        }
        

        public static String GetMD5(String str)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Accounts");
        }


        public ActionResult DetailUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.idType = new SelectList(db.TypeAccount, "id", "typeName", account.idType);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Account account, HttpPostedFileBase fileUpload)
        {

            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                var newFileName = Guid.NewGuid();
                var _extension = Path.GetExtension(fileUpload.FileName);
                string newName = newFileName + _extension;
                string _fileName = Path.GetFileName(newName);
                var _path = Path.Combine(Server.MapPath("~/ImagesUser"), _fileName);
                fileUpload.SaveAs(_path);
                account.imageUser = _fileName;
                if (ModelState.IsValid)
                {
                    db.Entry(account).State = EntityState.Modified;
                    account.display = true;
                    account.idType = (int) Session["typeAccount"];
                    account.status = "Hoạt động";
                    db.SaveChanges();
                    return RedirectToAction("DetailUser", "Accounts", new { id = Session["idUser"]});
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(account).State = EntityState.Modified;
                    account.display = true;
                    account.idType = (int) Session["typeAccount"];
                    account.status = "Hoạt động";
                    db.SaveChanges();
                    return RedirectToAction("DetailUser","Accounts",new { id = Session["idUser"]});
                }
            }
            ViewBag.idType = new SelectList(db.TypeAccount, "id", "typeName", account.idType);
            return View(account);
        }

        public void sendMail(string to)
        {
            Random rd = new Random();
            int number = rd.Next(100000, 999999);
            Session["numb"] = number;
            string from, pass, content;
            from = "studyweb.free@gmail.com";
            pass = "luepinittfinmcxr";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to.Trim());
            mail.From = new MailAddress(from);
            mail.Subject = "Website study free";
            mail.Body = "Mã xác nhận của bạn là: "+number;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string maill)
        {
            if (maill == null)
            {
                ViewData["null"] = "Không được bỏ trống mail!";
                return View();
            }
            else
            {
                sendMail(maill);
                var data = db.Account.Where(s => s.maill.Equals(maill)).ToList();
                if (data.Count > 0)
                {
                    ViewData["Success"] = "Kiểm tra mail của bạn để xác nhận!";
                    return RedirectToAction("ResetPass","Accounts", new { id = data.FirstOrDefault().id });
                }
                else
                {
                    ViewData["ErrorMail"] = "Mail không tồn tại!";
                    return View();
                }
            }


        }
        public ActionResult ResetPass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPass(int? id,string maill, string password, string respassword, string confirm)
        {
            Account account = db.Account.Find(id);
            string k = Session["numb"].ToString();


            if (confirm != k)
            {
                ViewData["Error1"] = "Mã xác nhận không chính xác!";
                return View(account);
            }
            else if(password != respassword)
            {
                ViewData["Error2"] = "Mật khẩu và không khớp!";
                return View(account);
            }
            else
            {
                account.password = GetMD5(password);
                db.SaveChanges();
                return RedirectToAction("Login", "Accounts");
            }

        }
    }
}
