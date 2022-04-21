using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.DataAccess;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class BugTrackerController : Controller
    {
        private DataOperations dataOperations = new DataOperations();
        // GET: BugTracker
        public ActionResult Index()
        {
            if (Session["username"] != null)
                return View(dataOperations.getBugs());
            else
                return RedirectToAction("Login", "LoginSystem");
        }
        public ActionResult AddBug()
        {
            if (Session["username"] != null)
                return View();
            else
                return RedirectToAction("Login", "LoginSystem");
        }
        [HttpPost]
        public ActionResult AddBug(Bug bug)
        {
            if (dataOperations.CreateBug(bug))
                return RedirectToAction("Index");
            else
                return View();
        }
        public ActionResult BugDetails(int id) {
            if(Session["username"] != null)
            return View(dataOperations.BugData(id));
            else
                return RedirectToAction("Login", "LoginSystem");
        }
        public ActionResult BugModify(int id) {
            if (Session["username"] != null) { 
                List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not yet solved", Value = "Not yet solved" });
            if((string)Session["authority"] != "programmer")
            {
                items.Add(new SelectListItem { Text = "Solved", Value = "Solved" });
            }
            items.Add(new SelectListItem { Text = "In progress", Value = "In progress"});
            ViewBag.Progress = items;

            return View(dataOperations.BugData(id));
            }
            else
                return RedirectToAction("Login", "LoginSystem");
        }
        [HttpPost]
        public ActionResult BugModify(Bug bug)
        {
            if (dataOperations.ModifyBug(bug))
                return RedirectToAction("BugDetails", new { id = bug.bugId });
            else
                return View();
        }
    }
}