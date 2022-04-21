using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BugTracker.Models;
using System.Web.Helpers;

namespace BugTracker.DataAccess
{
    public class BugTrackerContext : DbContext
    {
        public BugTrackerContext() { }
        public DbSet<Bug> bugs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserViewModel> userViewModels { get; set; }
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

        public bool userLogin(UserViewModel user) {
            var account = db.users.Where(x => x.accountName == user.accountName).FirstOrDefault();
            if (account != null)
            {
                if (Crypto.VerifyHashedPassword(account.password, user.password))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public string getAuthority(string accName) {
            var userData = db.users.Where(x => x.accountName == accName).FirstOrDefault();
            return userData.authority;
        }

        public bool register(UserViewModel user) {
            var userdata = db.users.Where(x => x.accountName == user.accountName);
            if (user.password != user.confirmPassword && userdata != null)
                return false;
            else
            {
                try
                {
                    User temp = new User();
                    temp.accountName = user.accountName;
                    temp.authority = user.authority;
                    temp.password = Crypto.HashPassword(user.password);
                    db.users.Add(temp);
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
}