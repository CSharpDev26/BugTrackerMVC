using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Bug
    {
        [Display(Name = "Id")]
        public int bugId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Progress")]
        public string progress { get; set; }
        [Display(Name = "Solution")]
        public string solution { get; set; }
    }
}