using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Speakster.Models
{
    public class Teacher
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Key]
        public string User_id { get; set; }

        //public ApplicationUser User { get; set; }
        public string First_name { get; set; }

        public string Last_name { get; set; }


        public int Language_id { get; set; }

        [ForeignKey("Language_id")]
        public Language Language { get; set; }



        public string Address { get; set; }

        public string CEP { get; set; }

        public string Phone { get; set; }

        public string ProfilePicture
        {
            get
            {
                string Picture = db.Users.Find(User_id).ProfilePicture;
                string Default = "~/images/Profile/avatar.png";
                return !String.IsNullOrEmpty(Picture) ? Picture : Default;
            }
        }

        public ChatMessage getLastMessage(string Student_id)
        {
            List<ChatMessage> messages = db.ChatMessages.Where(msg => msg.Teacher_id == User_id
                                               && msg.Student_id == Student_id).ToList();
            return messages.Any() ? messages.Last() : null;
        }

        public Teacher() { }  
        public Teacher(string User_id, string First_name, string Last_name, int Language_id)
        {
            this.User_id = User_id;
            this.First_name = First_name;
            this.Last_name = Last_name;
            this.Language_id = Language_id;
        }
    }
}