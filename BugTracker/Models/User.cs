using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class User
    {
        public int userId { get; set; }
        [Display(Name = "Username")]
        public string accountName { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string authority { get; set; }
    }
}