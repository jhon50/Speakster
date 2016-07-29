using Speakster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speakster.Controllers
{
    public class ValidationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Validation
        public JsonResult IsUserExists(string Email)
        {
            return IsExist(Email) ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }

        public bool IsExist(string Email)
        {
            var _model = (from s in db.Users
                          where s.Email == Email
                          select s.Email).FirstOrDefault();

            if (string.IsNullOrEmpty(Email)) return false;

            else if (_model != null)
                return false;
            else return true;
        }
    }
}