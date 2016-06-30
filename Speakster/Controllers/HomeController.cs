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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET
        public ActionResult Index()
        {
            ViewBag.Id = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Name")] Language language)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("DefineLevel", "Languages", new { LanguageID = language.Id });
            }
                return View(language);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}