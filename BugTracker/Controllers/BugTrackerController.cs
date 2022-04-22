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
            if (Session["username"] != null) {
                createProjectViewBag();
                return View();
            }
             else
                return RedirectToAction("Login", "LoginSystem");
        }
        [HttpPost]
        public ActionResult AddBug(Bug bug)
        {
            string p = Request.Form["projectalternative"];
            if (bug.project == "new project" && p != null)
                bug.project = p;
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
            if (Session["username"] != null)
            {
                List<SelectListItem> items = new List<SelectListItem>();
                    items.Add(new SelectListItem { Text = "Not yet solved", Value = "Not yet solved" });
                if ((string)Session["authority"] != "programmer")
                {
                    items.Add(new SelectListItem { Text = "Solved", Value = "Solved" });
                }
                items.Add(new SelectListItem { Text = "In progress", Value = "In progress" });
                ViewBag.Progress = items;

                createProjectViewBag();

                return View(dataOperations.BugData(id));
            }
            else
                return RedirectToAction("Login", "LoginSystem");
        }
        [HttpPost]
        public ActionResult BugModify(Bug bug)
        {
            string p = Request.Form["projectalternative"];
            if (bug.project == "new project" && p != null)
                bug.project = p;
            if (dataOperations.ModifyBug(bug))
                return RedirectToAction("BugDetails", new { id = bug.bugId });
            else
                return View();
        }
        private void createProjectViewBag() {
            List<SelectListItem> projectitems = new List<SelectListItem>();
            List<string> projectNames = dataOperations.getProjectNames();
            foreach (string name in projectNames)
            {
                projectitems.Add(new SelectListItem { Text = name, Value = name });
            }
            projectitems.Add(new SelectListItem { Text = "New project", Value = "new project" });
            ViewBag.Projects = projectitems;
        }
    }
}