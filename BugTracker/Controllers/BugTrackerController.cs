using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BugTracker.DataAccess;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class BugTrackerController : Controller
    {
        private DataOperations dataOperations = new DataOperations();

        public ActionResult Index(string sortParam)
        {
            if (Session["username"] != null)
            {
                ViewBag.NameSortParam = String.IsNullOrEmpty(sortParam) ? "name_desc" : "";
                ViewBag.ProjectSortParam = sortParam == "project" ? "project_desc" : "project";
                ViewBag.ProgressSortParam = sortParam == "progress" ? "progress_desc" : "progress";
                ViewBag.IdSortParam = sortParam == "bugId" ? "bugId_desc" : "bugId";
                var bugs = dataOperations.getBugs();
                switch (sortParam)
                {
                    case "name_desc":
                        bugs = bugs.OrderByDescending(b => b.name);
                        break;
                    case "project_desc":
                        bugs = bugs.OrderByDescending(b => b.project);
                        break;
                    case "project":
                        bugs = bugs.OrderBy(b => b.project);
                        break;
                    case "progress_desc":
                        bugs = bugs.OrderByDescending(b => b.progress);
                        break;
                    case "progress":
                        bugs = bugs.OrderBy(b => b.progress);
                        break;
                    case "bugId":
                        bugs = bugs.OrderBy(b => b.bugId);
                        break;
                    case "bugId_desc":
                        bugs = bugs.OrderByDescending(b => b.bugId);
                        break;
                    default:
                        bugs = bugs.OrderBy(b => b.name);
                        break;
                }
                return View(bugs);
            }
            else
                return RedirectToAction("Login", "LoginSystem");
        }

        public ActionResult AddBug()
        {
            if (Session["username"] != null)
            {
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
            {
                createProjectViewBag();
                ViewBag.Error = "Incorrect data!";
                return View(bug);
            }
        } 

        public ActionResult BugDetails(int id) {
            if (Session["username"] != null)
                return View(dataOperations.BugData(id));
            else
                return RedirectToAction("Login", "LoginSystem");
        }

        public ActionResult BugModify(int id)
        {
            if (Session["username"] != null)
            {

                createProgressViewBag();
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
            {
                createProgressViewBag();
                createProjectViewBag();
                ViewBag.Error = "Incorrect data!"; 
                return View(bug);
            }
                
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

        private void createProgressViewBag() {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not yet solved", Value = "Not yet solved" });
            if ((string)Session["authority"] != "programmer")
            {
                items.Add(new SelectListItem { Text = "Solved", Value = "Solved" });
            }
            items.Add(new SelectListItem { Text = "In progress", Value = "In progress" });
            ViewBag.Progress = items;
        }
    }
}