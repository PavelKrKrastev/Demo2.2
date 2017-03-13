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
using PagedList;

namespace ManageEmployees.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Employees
        //Sorting:Index receives a sortOrder parameter from the query string.The query string value is provided as a parameter to the action method.
        //Searching:Index receives searchString parameter.
        //Added paging using the PagedList.Mvc NuGet package.Index receives currentFilter and page
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //The ViewBag variables are used so that the view can configure the appropriate query string values
            //The ternary statements specify that if the sortOrder parameter is null or empty "name_desc" should be specified. Otherwise, it should be set to an empty string.
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PositionSort = sortOrder == "Position" ? "Position_desc" : "Position";
            //Provides the view with the current sort order, in order to keep the sort order the same while paging
            ViewBag.CurrentSort = sortOrder;


            //currentFilter, provides the view with the current filter string, to maintain the filter settings during paging.
            //It must be restored to the text box when the page is redisplayed.
            //If the search string is changed during paging, the page has to be reset to 1, because the new filter can result in different data to display.
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            //provides the view with the current filter string.
            ViewBag.CurrentFilter = searchString;

            var employee = from s in db.Employees
                           select s;

            //LINQ statement selects only students whose first name or last name contains the search string.
            if (!String.IsNullOrEmpty(searchString))
            {
                employee = employee.Where(s =>
                    s.FirstName.ToUpper().Contains(searchString.ToUpper())
                    || s.LastName.ToUpper().Contains(searchString.ToUpper()));
            }

            //The method uses LINQ to Entities to specify the column to sort by. Switch statement, modifies the sort order.
            switch (sortOrder)
            {
                //Order by FirstName descending
                case "name_desc":
                    employee = employee.OrderByDescending(s => s.FirstName);
                    break;
                //Order by salary
                case "Position":
                    employee = employee.OrderBy(s => s.Position);
                    break;
                //Order by salary descending
                case "Position_desc":
                    employee = employee.OrderByDescending(s => s.Position);
                    break;
                // Default order by FirstName ascending
                default:
                    employee = employee.OrderBy(s => s.FirstName);
                    break;
            }
            //Define page size
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //ToPagedList extension method query to a single page collection supports paging. That single page to the view.
            return View(employee.ToPagedList(pageNumber, pageSize));
        }

        // GET: Detail about the entered employees
        public ActionResult Details(int? id)
        {
            //Check if employee exists
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Create new employees
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create new employees
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,Position,Salary,City,Phone")] Employee employee)
        {
            //Try to create a new employee
            try
            {
                //Check if entered values are valid
                if (ModelState.IsValid)
                {
                    //Check if CEO exists befor creating
                    if (employee.Position != EmployeePosition.CEO)
                    {
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //Display error message to user
                        ModelState.AddModelError("", "The company already has a CEO!");
                    }
                }
            }
            //Catch exception if the data is not saved
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }

        // GET: Create new employees
        public ActionResult Edit(int? id)
        {
            //Check to see if employee exists
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            //Configure the appropriate values to pass to dropdown list in view
            //Check to set values for the viewbag bind to the dropdownlist to contain correct values
            if (employee.Position == EmployeePosition.CEO || employee.Position == EmployeePosition.Delivery_Director)
            {
                ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => p.Position.ToString() == "CEO"), "ID", "FirstName", employee.EmployeeId);
            }
            else if (employee.Position == EmployeePosition.Project_Manager)
            {
                ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => p.Position.ToString() == "Delivery_Director"), "ID", "FirstName", employee.EmployeeId);
            }
            else if (employee.Position == EmployeePosition.Team_Leader) 
            {
                ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => p.Position.ToString() == "Project_Manager"), "ID", "FirstName", employee.EmployeeId);
            }
            else
            {
                ViewBag.EmployeeId = new SelectList(db.Employees.Where(p => p.Position.ToString() == "Team_Leader"), "ID", "FirstName", employee.EmployeeId);
            }
            //Provides the view with information about the temas. 
            ViewBag.TeamId = new SelectList(db.Teams, "ID", "Name", employee.TeamID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Position,Salary,City,Phone,EmployeeId,TeamID")] Employee employee)
        {
            //Try to edit employee
            try
            {
                //Check if entered values are valid
                if (ModelState.IsValid)
                {
                    //Check that CEO,DD,PM are not assigned to temas
                    if ((employee.Position == EmployeePosition.CEO && employee.TeamID != null) || (employee.Position == EmployeePosition.Project_Manager && employee.TeamID != null)
                        || (employee.Position == EmployeePosition.Delivery_Director && employee.TeamID != null))
                    {
                        ModelState.AddModelError("", "Only people bellow team leaders can be assigned to a team!");
                    }
                    //TL can't be moved to new teams
                    else if (employee.Position == EmployeePosition.Team_Leader && employee.TeamID == null)
                    {
                        ModelState.AddModelError("", "Team leaders can't be moved!");
                    }
                    //Save changes
                    else
                    {
                        db.Entry(employee).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index"); ;
                    }
                }
            }
            //Catch exception if the data is not saved
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            //Configure the appropriate values passed from the view dropdown
            ViewBag.EmployeeId = new SelectList(db.Employees, "ID", "FirstName", employee.EmployeeId);
            ViewBag.TeamId = new SelectList(db.Teams, "ID", "Name", employee.TeamID);
            return View(employee);

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
