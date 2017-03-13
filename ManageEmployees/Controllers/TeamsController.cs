using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageEmployees.DAL;
using ManageEmployees.Models;

namespace ManageEmployees.Controllers
{
    public class TeamsController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: List of teams 
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: Detail for teams
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Create teams
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create teams
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Project,ProjectManager")] Team team)
        {
            //Try to create teams
            try
            {
                //Check if entered values are valid
                if (ModelState.IsValid)
                {
                    //Save changes
                    db.Teams.Add(team);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            //Catch exception if the data is not saved
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(team);
        }

        // GET: Edit teams
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Edir teams
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Project,ProjectManager")] Team team)
        {
            //Try to edit teams
            try
            {
                //Check if entered values are valid
                if (ModelState.IsValid)
                {
                    //Save changes
                    db.Entry(team).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            //Catch exception if the data is not saved
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(team);
        }
        //Dispose the connection to the database
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
