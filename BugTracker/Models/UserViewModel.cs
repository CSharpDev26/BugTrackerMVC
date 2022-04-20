using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class UserViewModel
    {
        [Key]
        public int userId { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string accountName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Authority")]
        public string authority { get; set; }
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}