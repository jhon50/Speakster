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
    public class SpeakingLevelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpeakingLevels
        public ActionResult Index()
        {
            return View(db.SpeakingLevels.ToList());
        }

        // GET: SpeakingLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeakingLevel speakingLevel = db.SpeakingLevels.Find(id);
            if (speakingLevel == null)
            {
                return HttpNotFound();
            }
            return View(speakingLevel);
        }

        // GET: SpeakingLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpeakingLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] SpeakingLevel speakingLevel)
        {
            if (ModelState.IsValid)
            {
                db.SpeakingLevels.Add(speakingLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(speakingLevel);
        }

        // GET: SpeakingLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeakingLevel speakingLevel = db.SpeakingLevels.Find(id);
            if (speakingLevel == null)
            {
                return HttpNotFound();
            }
            return View(speakingLevel);
        }

        // POST: SpeakingLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] SpeakingLevel speakingLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speakingLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(speakingLevel);
        }

        // GET: SpeakingLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpeakingLevel speakingLevel = db.SpeakingLevels.Find(id);
            if (speakingLevel == null)
            {
                return HttpNotFound();
            }
            return View(speakingLevel);
        }

        // POST: SpeakingLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpeakingLevel speakingLevel = db.SpeakingLevels.Find(id);
            db.SpeakingLevels.Remove(speakingLevel);
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
