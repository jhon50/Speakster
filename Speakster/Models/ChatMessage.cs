using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Speakster.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool TeacherRead { get; set; }

        public bool StudentRead { get; set; }

        public string Student_id { get; set; }

        public string Teacher_id { get; set; }

        public string Sender { get; set; }


    }
}