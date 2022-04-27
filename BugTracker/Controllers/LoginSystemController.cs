using System.Collections.Generic;
using System.Web.Mvc;
using BugTracker.Models;
using BugTracker.DataAccess;
namespace BugTracker.Controllers
{
    public class LoginSystemController : Controller
    {
        private DataOperations dataOperations = new DataOperations();

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
            else {
                ViewBag.Error = "Incorrect data, please check it again!";
                return View(user);
            }
        }

        public ActionResult Register()
        {
            if ((string)Session["authority"] == "admin")
            {
                createViewBag();
                return View();
            }
            else
            {
                Session.Abandon();
                return RedirectToAction("Login", "LoginSystem");
            }
        }

        [HttpPost]
        public ActionResult Register(UserViewModel user)
        {
            if (dataOperations.register(user))
            {
                return RedirectToAction("Index", "BugTracker");
            }
            else {
                createViewBag();
                ViewBag.Error = "Incorrect data, please check it again!";
                return View(user);
            }
                
        }

        public ActionResult LogOut() {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        private void createViewBag() {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Manager", Value = "manager" });
            items.Add(new SelectListItem { Text = "Programmer", Value = "programmer" });
            items.Add(new SelectListItem { Text = "Admin", Value = "admin" });
            items.Add(new SelectListItem { Text = "Tester", Value = "tester" });
            ViewBag.Auth = items;
        }
    }
}