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

        public ActionResult Index()
        {
            return View();
        }
        
        
        //GET
        public ActionResult HomeLanguages()
        {
            ViewBag.Id = new SelectList(db.Languages, "Id", "Name");
            return PartialView("_HomeLanguages");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HomeLanguages([Bind(Include = "Id,Name")] Language language)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("DefineLevel", "Languages", new { LanguageID = language.Id });
            }
                return PartialView("_HomeLanguage", language);
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