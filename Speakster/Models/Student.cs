using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
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

        public string Email { get { return getEmail(); } }

        public Student() {}

        public Student(string User_id, string First_name, string Last_name)
        {
            this.User_id = User_id;
            this.First_name = First_name;
            this.Last_name = Last_name;
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

    }
}