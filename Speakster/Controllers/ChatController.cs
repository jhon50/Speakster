using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Speakster.Models;
using System.Data.Entity;
using System.Net;

namespace Speakster.Controllers
{
    public class ChatController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chat
        public ActionResult Student()
        {
            string User_id = User.Identity.GetUserId();
            DateTime pastDate = DateTime.Now.AddMonths(-1);
            //I'm a Student
            Student student = db.Students.Find(User_id);
            var messages = db.ChatMessages.Where(msg => msg.Date > pastDate
                                                && msg.Student_id == User_id
                                                && msg.Teacher_id == student.Teacher_id
                                                );

            ViewBag.Messages = (messages.Any()) ? messages : null;
            return View();
        }

        // POST: Chat
        [HttpPost]
        public ActionResult Student(string message)
        {
            SendMessageAsStudent(message);
            return View();
        }

        // GET: Chat
        public ActionResult Teacher()
        {
            string Student_id = Request["Student_id"];
            string User_id = User.Identity.GetUserId();
            DateTime pastDate = DateTime.Now.AddMonths(-1);

            //I'm a Teacher
            //Retrieve teacher messages to respective student
            var messages = db.ChatMessages.Where(msg => msg.Date > pastDate
                                                && msg.Teacher_id == User_id
                                                && msg.Student_id == Student_id
                                                );
            ViewBag.Messages = (messages.Any()) ? messages : null;
            ViewBag.Student_id = Student_id;
            return View();
        }

        // POST: Chat
        [HttpPost]
        public ActionResult Teacher(string message, string Student_id)
        {
            string User_id = User.Identity.GetUserId();
            SendMessageAsTeacher(message, Student_id);
            return View();
        }

        private void SendMessageAsStudent(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                string Id = User.Identity.GetUserId();
                Student user = db.Students.Find(Id);

                //Seta a FLAG de nova mensagem TRUE
                ApplicationUser teacher = db.Users.Find(user.Teacher_id);
                teacher.NewMessage = true;
                db.Entry(teacher).State = EntityState.Modified;

                ChatMessage chat = new ChatMessage();
                chat.Date = DateTime.Now;
                chat.Message = message;
                chat.Student_id = Id;
                chat.Teacher_id = teacher.Id;
                chat.Sender = User.Identity.GetUserId();
                db.ChatMessages.Add(chat);
                db.SaveChanges();
            }
        }

        private void SendMessageAsTeacher(string message, string Student_id)
        {
            if (!string.IsNullOrEmpty(message))
            {
                //Seta a FLAG de nova mensagem TRUE
                ApplicationUser student = db.Users.Find(Student_id);
                student.NewMessage = true;
                db.Entry(student).State = EntityState.Modified;

                ChatMessage chat = new ChatMessage();
                chat.Date = DateTime.Now;
                chat.Message = message;
                chat.Student_id = Student_id;
                chat.Teacher_id = User.Identity.GetUserId();
                chat.Sender = User.Identity.GetUserId();
                db.ChatMessages.Add(chat);
                db.SaveChanges();
            }
        }
        public ActionResult ReturnTeacherNewMessages()
        {
            string Student_id = Request["Student_id"];
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            string Teacher_id = user.Id;
            
            //I'm a Teacher
            //Retrieve teacher messages to respective student
            var messages = db.ChatMessages.Where(msg => msg.Teacher_id == Teacher_id
                                                && msg.Student_id == Student_id
                                                );

            ViewBag.Messages = (messages.Any()) ? messages : null;

            //Seta a FLAG como FALSE após o usuário receber as mensagens
            user.NewMessage = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return View();
        }

        public ActionResult ReturnStudentNewMessages()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            Student student = db.Students.Find(user.Id);
            var messages = db.ChatMessages.Where(msg => msg.Student_id == student.User_id
                                                && msg.Teacher_id == student.Teacher_id
                                                );

            ViewBag.Messages = (messages.Any()) ? messages : null;

            //Seta a FLAG como FALSE após o usuário receber as mensagens
            user.NewMessage = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return View();
        }

        public ActionResult NewMessageStatus()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            if(user.NewMessage == true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }else
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);

            }
        }



        //public ActionResult ReturnMsg()
        //{
        //    string User_id = User.Identity.GetUserId();
        //    string Student_id = Request["Student_id"];
        //    //ViewBag.Student_id = Student_id;

        //    if (Student_id != null)
        //    {
                
        //    }
        //    else
        //    {
        //        //I'm a Student
                
        //        foreach (var item in messages)
        //        {
        //            item.StudentRead = true;
        //        }
        //        db.SaveChanges();
        //        return View();
        //    }


        //    //DateTime pastDate = DateTime.Now.AddMonths(-1);

        //    //if (Student_id != null)
        //    //{
        //    //    //I'm a Teacher
        //    //    //Retrieve teacher messages to respective student
        //    //    var messages = db.ChatMessages.Where(msg => msg.Date > pastDate
        //    //                                        && msg.Teacher_id == User_id
        //    //                                        && msg.Student_id == Student_id
        //    //                                        );
        //    //    ViewBag.Messages = (messages.Any()) ? messages : null;
        //    //    return View();
        //    //}
        //    //else
        //    //{
        //    //    //I'm a Student
        //    //    Student student = db.Students.Find(User_id);
        //    //    var messages = db.ChatMessages.Where(msg => msg.Date > pastDate
        //    //                                        && msg.Student_id == User_id
        //    //                                        && msg.Teacher_id == student.Teacher_id
        //    //                                        );

        //    //    ViewBag.Messages = (messages.Any()) ? messages : null;
        //    //    return View();
        //    //}




        //    //foreach (var item in messagem)
        //    //{
        //    //    if (item.MSG_PERSONAL)
        //    //    {
        //    //        item.MSG_READ = true;
        //    //    }
        //    //}
        //}

       
    }
}