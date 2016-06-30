using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Speakster.Models;

namespace Speakster.Controllers
{
    public class ListeningLevelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ListeningLevels
        public ActionResult Index()
        {
            return View(db.ListeningLevels.ToList());
        }

        // GET: ListeningLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListeningLevel listeningLevel = db.ListeningLevels.Find(id);
            if (listeningLevel == null)
            {
                return HttpNotFound();
            }
            return View(listeningLevel);
        }

        // GET: ListeningLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListeningLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] ListeningLevel listeningLevel)
        {
            if (ModelState.IsValid)
            {
                db.ListeningLevels.Add(listeningLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(listeningLevel);
        }

        // GET: ListeningLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListeningLevel listeningLevel = db.ListeningLevels.Find(id);
            if (listeningLevel == null)
            {
                return HttpNotFound();
            }
            return View(listeningLevel);
        }

        // POST: ListeningLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] ListeningLevel listeningLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listeningLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(listeningLevel);
        }

        // GET: ListeningLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListeningLevel listeningLevel = db.ListeningLevels.Find(id);
            if (listeningLevel == null)
            {
                return HttpNotFound();
            }
            return View(listeningLevel);
        }

        // POST: ListeningLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListeningLevel listeningLevel = db.ListeningLevels.Find(id);
            db.ListeningLevels.Remove(listeningLevel);
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
