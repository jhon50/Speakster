using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Speakster.Models
{
    public class Student
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Key]
        public string User_id { get; set; }

        public string Teacher_id { get; set; }

        [ForeignKey("Teacher_id")]
        public virtual Teacher Teacher { get; set; }

        [Display(Name = "Nome")]
        public string First_name { get; set; }

        [Display(Name = "Sobrenome")]
        public string Last_name { get; set; }

        [Display(Name = "Nível de Fala")]
        public string SpeakingLevel { get { return getSpeakingLevel(); } }

        [Display(Name = "Nível de Entendimento")]
        public string ListeningLevel { get { return getListeningLevel(); } }

        [Display(Name = "Idioma Escolhido")]
        public string Language { get { return getLanguage(); } }

        public string Email { get { return getEmail(); } }

        public Student() {}

        public Student(string User_id, string First_name, string Last_name)
        {
            this.User_id = User_id;
            this.First_name = First_name;
            this.Last_name = Last_name;
        }

        private string getSpeakingLevel()
        {
            ApplicationUser user = db.Users.Find(User_id);
            return user.SpeakingLevel.Description;
        }

        private string getListeningLevel()
        {
            ApplicationUser user = db.Users.Find(User_id);
            return user.ListeningLevel.Description;
        }

        private string getLanguage()
        {
            ApplicationUser user = db.Users.Find(User_id);
            return user.Language.Name;
        }

        public string getEmail()
        {
            ApplicationUser user = db.Users.Find(User_id);
            return user.Email;
        }

        public bool IsActive()
        {
            UserPayment user_payment = db.UserPayments.Find(User_id);
            if(user_payment != null && user_payment.Payment_status == "Completed")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ChatMessage getLastMessage()
        {
            List<ChatMessage> messages = db.ChatMessages.Where(msg => msg.Teacher_id == Teacher_id
                                               && msg.Student_id == User_id
                                               && msg.Sender == User_id).ToList();
            return messages.Any() ? messages.Last() : null;
        }

        public bool hasTeacher()
        {
            return Teacher_id != null ? true : false;
        }

        public Teacher getTeacher()
        {
            return db.Teachers.Find(Teacher_id);
        }

        public string FullName
        {
            get
            {
                return First_name + " " + Last_name;
            }
        }

        public string ProfilePicture
        {
            get
            {
                string Picture = db.Users.Find(User_id).ProfilePicture;
                string Default = "~/images/Profile/avatar.png";
                return !String.IsNullOrEmpty(Picture) ? Picture : Default;
            }
        }

    }
}