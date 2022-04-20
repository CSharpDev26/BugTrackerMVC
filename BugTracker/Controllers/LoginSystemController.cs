using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.DataAccess;
namespace BugTracker.Controllers
{
    public class LoginSystemController : Controller
    {
        private DataOperations dataOperations = new DataOperations();
        // GET: LoginSystem
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (dataOperations.userLogin(user))
                return RedirectToAction("Index", "BugTracker");
            else
                return View(user);
        }
        public ActionResult Register()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "manager", Value = "manager" });
            items.Add(new SelectListItem { Text = "programmer", Value = "programmer" });
            ViewBag.Auth = items;
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            if (dataOperations.register(user)) //login
                return RedirectToAction("Index", "BugTracker");
            else//message?
                return View();
        }
    }
}