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
    public class LanguagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Languages
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        // GET: Languages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Language language)
        {
            if (ModelState.IsValid)
            {
                db.Languages.Add(language);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(language);
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Language language)
        {
            if (ModelState.IsValid)
            {
                db.Entry(language).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: Languages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Language language = db.Languages.Find(id);
            db.Languages.Remove(language);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        //Recebe o idioma selecionado pelo usuário na página inicial
        //Geral a listagem do Levels de Fala e Escuta
        //GET: Languages/DefineLevel
        public ActionResult DefineLevel()
        {
            if (!String.IsNullOrEmpty(Request["LanguageID"]))
            {
                string LanguageID = Request["LanguageID"];
                int id = Int32.Parse(LanguageID);
                var language = db.Languages.Where(n => n.Id == id);
                Language chosenLanguage = language.SingleOrDefault();
                ViewBag.ChosenLanguageName = chosenLanguage.Name;
                ViewBag.Language_id = id;
            }
            ViewBag.ListeningLevel_id = new SelectList(db.ListeningLevels, "Id", "Description");
            ViewBag.SpeakingLevel_id = new SelectList(db.SpeakingLevels, "Id", "Description");
            return View();
        }

        [HttpPost]
        //POST: Languages/DefineLevel
        public ActionResult DefineLevel(ApplicationUser applicationUser, string languageID)
        {
            //
            //Passa os parametros selecionados pelo usuário para a criação da conta
            if (ModelState.IsValid)
            {
                return RedirectToAction("Register", "Account", new { languageID = languageID, speakingLevelID = applicationUser.SpeakingLevel_id, listeningLevelID = applicationUser.ListeningLevel_id, returnUrl = "/Users/Checkout" });
            }
            return View(applicationUser);
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
