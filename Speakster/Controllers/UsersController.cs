using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Speakster.Models;
using System.Threading.Tasks;

namespace Speakster.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        [Auth(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var applicationUsers = db.Users.Include(a => a.Language).Include(a => a.ListeningLevel).Include(a => a.SpeakingLevel);
            return View(applicationUsers.ToList());
        }

        [Auth]
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            if (User.IsInRole("TEACHER"))
            {
                string User_Id = User.Identity.GetUserId();
                var students = db.Students.Where(s => s.Teacher_id == User_Id).ToList();
                List<Student> result = students.FindAll(s => s.IsActive());
                return View("TeacherDashboard", result);
                //return View("TeacherDashboard");
            }
            else if (User.IsInRole("ADMIN"))
            {
                return View("ManagerDashboard");
            }
            else
            {
                return View("UserDashboard");
            }
        }

        [Auth]
        //GET: Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        [Auth]
        public async Task<ActionResult> Confirmation()
        {
            string code = "";
            if (User.Identity.IsAuthenticated)
            {
                //Envia um email com os dados da transação
                Email email = new Email();
                string UserId = User.Identity.GetUserId();
                Student student = db.Students.Find(UserId);
                ApplicationUser user = db.Users.Find(UserId);
                UserPayment payment = db.UserPayments.Find(UserId);
                if (payment != null)
                {
                    code = payment.Txn_id;
                    string text = "Seu pagamento foi confirmado e sua recorrência está ativa." +
                              "O código da sua transação é: " + code;

                    await email.Send(user.Email, student.FullName, "Confirmação de pagamento Speakster", "codigo da transação: " + code,
                        "Olá " + student.First_name + ". Seu pagamento foi confirmado! <br/> Por favor, guarde o código da sua transação para sua segurança! <br/>"
                        + "Código da transação: " + code + "<br/> Caso tenha alguma dúvida entre em contato com nosso suporte! <br/> Estamos muito felizes por ter você como nosso aluno! <br/> <a href='http://speakster.com.br'>SPEAKSTER</a> ");
                    return View();
                }
            }
            return RedirectToAction("Dashboard", "Users");
        }

        [Auth]
        public ActionResult PaymentDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserPayment userPayment = db.UserPayments.Find(id);
            if (userPayment.isActive())
            {
                ViewBag.isActive = "Ativa";
            }
            else
            {
                ViewBag.isActive = "Inativa";
            }

            if (userPayment == null)
            {
                return HttpNotFound();
            }
            return View(userPayment);
        }

        [Auth(Roles = "ADMIN")]
        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [Auth(Roles = "ADMIN")]
        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name");
            ViewBag.ListeningLevel_id = new SelectList(db.ListeningLevels, "Id", "Description");
            ViewBag.SpeakingLevel_id = new SelectList(db.SpeakingLevels, "Id", "Description");
            return View();
        }

        [Auth(Roles = "ADMIN")]
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Language_id,SpeakingLevel_id,ListeningLevel_id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name", applicationUser.Language_id);
            ViewBag.ListeningLevel_id = new SelectList(db.ListeningLevels, "Id", "Description", applicationUser.ListeningLevel_id);
            ViewBag.SpeakingLevel_id = new SelectList(db.SpeakingLevels, "Id", "Description", applicationUser.SpeakingLevel_id);
            return View(applicationUser);
        }

        //POST: Users/ProfilePicture
        [HttpPost]
        public ActionResult ProfilePicture([Bind(Include = "id")]ChangeProfilePictureViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.Users.Find(model.id);
                foreach (string fileName in Request.Files)
                {
                    var imagens = Request.Files[fileName];
                    if (imagens.ContentLength != 0)
                    {
                        string[] extension = imagens.FileName.Split('.');
                        string namePicture = GenerateNamePicture() + "." + extension[1];
                        HttpPostedFileBase postedFile = Request.Files[fileName];
                        postedFile.SaveAs(Server.MapPath("~/Uploads/ProfilePictures/") + namePicture);
                        string name = user.UserName;
                        user.ProfilePicture = "/Uploads/ProfilePictures/" + namePicture;

                    }
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect("" + Request.UrlReferrer);
        }

        //Gera um nome aleatorio para a foto do aluno
        public string GenerateNamePicture()
        {
            int length = 16;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            System.Random rnd = new System.Random();
            while (length-- > 0)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }

        [Auth(Roles = "ADMIN")]
        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name", applicationUser.Language_id);
            ViewBag.ListeningLevel_id = new SelectList(db.ListeningLevels, "Id", "Description", applicationUser.ListeningLevel_id);
            ViewBag.SpeakingLevel_id = new SelectList(db.SpeakingLevels, "Id", "Description", applicationUser.SpeakingLevel_id);
            return View(applicationUser);
        }

        [Auth(Roles = "ADMIN")]
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Language_id,SpeakingLevel_id,ListeningLevel_id,Teacher_Id,Active,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser, Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name", applicationUser.Language_id);
            ViewBag.ListeningLevel_id = new SelectList(db.ListeningLevels, "Id", "Description", applicationUser.ListeningLevel_id);
            ViewBag.SpeakingLevel_id = new SelectList(db.SpeakingLevels, "Id", "Description", applicationUser.SpeakingLevel_id);
            return View(applicationUser);
        }

        // GET: Users/Delete/5
        [Auth(Roles = "ADMIN")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [Auth(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
