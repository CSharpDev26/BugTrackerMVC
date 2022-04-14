using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BugTracker.Models;

namespace BugTracker.DataAccess
{
    public class BugTrackerContext : DbContext
    {
        public BugTrackerContext() { }
        public DbSet<Bug> bugs { get; set; }
    }

    public class DataOperations
    {
        public BugTrackerContext db = new BugTrackerContext();

        public IOrderedQueryable<Bug> getBugs() {
            var bugs = from e in db.bugs
                       orderby e.bugId
                       select e;
            return bugs;
        }
        public bool CreateBug(Bug bug) {
            try
            {
                bug.progress = "Not yet solved";
                bug.solution = "Not yet solved";
                db.bugs.Add(bug);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Bug BugData(int id) {
            var bug = from e in db.bugs
                      where e.bugId == id
                      select e;
            return bug.SingleOrDefault();
        }
        public bool ModifyBug(Bug bug)
        {
            try
            {
                Bug dbBug = db.bugs.Where(b => b.bugId == bug.bugId).First();
                dbBug.description = bug.description;
                dbBug.name = bug.name;
                dbBug.progress = bug.progress;
                dbBug.solution = bug.solution;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}