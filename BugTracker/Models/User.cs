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
        public string accountName { get; set; }
        public string password { get; set; }
        public string authority { get; set; }
    }
}