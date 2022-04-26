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
        public ActionResult Login(UserViewModel user)
        {
            if (dataOperations.userLogin(user))
            {
                Session["username"] = user.accountName;
                Session["authority"] = dataOperations.getAuthority(user.accountName);
                return RedirectToAction("Index", "BugTracker");
            }
            else
                return View(user);
        }
        public ActionResult Register()
        {
            //if ((string)Session["authority"] == "admin")
            //{
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Manager", Value = "manager" });
                items.Add(new SelectListItem { Text = "Programmer", Value = "programmer" });
                items.Add(new SelectListItem { Text = "Admin", Value = "admin" });
                items.Add(new SelectListItem { Text = "Tester", Value = "tester" });
                ViewBag.Auth = items;
                return View();
            //}
            //else
            //{
            //    Session.Abandon();
            //    return RedirectToAction("Login", "LoginSystem");
            //}
               
        }
        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            if (dataOperations.register(user))
            { 
                return RedirectToAction("Index", "BugTracker");
            }
            else
                return View();
        }
        public ActionResult LogOut() {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}