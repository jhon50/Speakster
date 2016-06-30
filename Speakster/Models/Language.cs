using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Speakster.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Name { get; set; }
    }
}