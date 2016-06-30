using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class TeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Language);
            return View(teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_id,First_name,Last_name,Language_id,Address,CEP,Phone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                AddUserToRole(teacher.User_id, "TEACHER");
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name", teacher.Language_id);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name", teacher.Language_id);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_id,First_name,Last_name,Language_id,Address,CEP,Phone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Language_id = new SelectList(db.Languages, "Id", "Name", teacher.Language_id);
            return View(teacher);
        }

        //GET: Teachers/Gallery
        public ActionResult Gallery()
        {
            var teachers = db.Teachers.Include(t => t.Language);
            return View(teachers.ToList());
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            RemoveUserFromRole(teacher.User_id, "TEACHER");
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

        internal void AddUserToRole(string userId, string roleName)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            try
            {
                UserManager.AddToRole(userId, roleName);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        internal void RemoveUserFromRole(string userId, string roleName)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            try
            {
                UserManager.RemoveFromRole(userId, roleName);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
