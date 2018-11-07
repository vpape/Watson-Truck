using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;


namespace Watson.Controllers
{
    //[Authorize]
    public class EmployeeController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> employee = new List<Employee>();

        public EmployeeController()
        {
            employee.Add(new Employee { SSN = "0001", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "Admin", JobTitle = "Analyst", User_id = 1 });
            employee.Add(new Employee { SSN = "0002", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "Admin", JobTitle = "HR Manager", User_id = 2 });
            employee.Add(new Employee { SSN = "0003", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "Admin", JobTitle = "HR Manager", User_id = 3 });
        }

        //GET: Employee
        public JsonResult EmployeeOverview()
        {
            var output = (from e in db.Employees
                          select new
                          {
                             e.User_id,
                             e.SSN,
                             e.JobTitle,
                             e.FirstName,
                             e.LastName,
                             e.MailingAddress,
                             e.City,
                             e.State,

                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }

        //GET: Employee/5
        public JsonResult EmployeeOverview(int id)
        {
            Employee e = db.Employees
                .Where(i => i.User_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        // GET: api/Employee
        //I changed List<Employee> to List<string>
        //[System.Web.Http.Route("api/Employee/EmployeeOverview/{User_id:int}/{SSN:string}")]
        //[System.Web.Http.Route("api/Employee/EmployeeOverview")]
        //[System.Web.Http.HttpGet]
        //public List<Employee> EmployeeOverview()
        //{
        //    List<string> output = new List<string>();

        //    foreach (var e in employee)
        //    {
        //        output.Add(e.SSN);
        //        output.Add(e.FirstName);
        //        output.Add(e.LastName);
        //        output.Add(e.JobTitle);
        //        output.Add(e.MailingAddress);
        //        output.Add(e.City);
        //        output.Add(e.State);
        //    }

        //    return employee;
        //}

        // GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/EmployeeOverview/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee EmployeeOverview(int id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}
        //----------------------------------------------------------------------------------------


        // GET: Employee
        //Database column "MartialStatus" is spelled incorrectly...need to fix
        public JsonResult EmployeeEnrollment()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.User_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Sex,
                              e.MartialStatus,
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }
        
        //GET: Employee/5
        public JsonResult EmployeeEnrollment(int id)
        {
            Employee e = db.Employees
                .Where(i => i.User_id == id)
                .SingleOrDefault();


            db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------
        // GET: api/Employee
        //[System.Web.Http.Route("api/Employee/EmployeeEnrollment")]
        //[System.Web.Http.HttpGet]
        //public List<Employee> EmployeeEnrollment()
        //{
        //    return employee;
        //}

        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/EmployeeEnrollment/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee EmployeeEnrollment(int id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}


        // POST: api/Employee
        //[System.Web.Http.Route("api/Employee/EmployeeEnrollment")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EmployeeEnrollment([Bind(Include = "User_id,CurrentEmployer,JobTitle,SSN,FirstName,LastName,DateOfBirth," +
        //    "Sex,MartialStatus")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Employees.Add(employee);
        //        db.SaveChanges();
        //    }

        //    return View(employee);
        //}

        // PUT: api/Employee/5
        //public void UpdateEmployee(int id, Employee value)
        //{
        //    employee[id] = value;
        //}
        //----------------------------------------------------------------------------------------


        // GET: Employee
        public JsonResult Contact()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.User_id,
                              e.MailingAddress,
                              e.PhysicalAddress,
                              e.City,
                              e.State,
                              e.ZipCode,
                              e.County,
                              e.CityLimits,
                              e.EmailAddress,
                              e.PhoneNumber,
                              e.CellPhone,
                          });
            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
                          
        }

        // GET: Employee/5
        public JsonResult Contact(int id)
        {
            Employee e = db.Employees
                .Where(i => i.User_id == id)
                .SingleOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------
        // GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Contact/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public ActionResult Contact(int? id)
        //{
        //    Employee employee = db.Employees.Find(id);

        //    return View(employee);
        //}

        // POST: api/Employee
        //[System.Web.Http.Route("api/Employee/Contact")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Contact([Bind(Include = "User_id,MailingAddress,PhysicalAddress,City,State,ZipCode,County,CityLimits,EmailAddress" +
        //    "PhoneNumber,CellPhone")] Employee contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Employees.Add(contact);
        //        db.SaveChanges();
        //    }

        //    return View(contact);
        //}
        //----------------------------------------------------------------------------------------

        // GET: Employee
        public JsonResult Edit()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.User_id,
                              e.CurrentEmployer,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Sex,
                              e.MartialStatus,
                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        // GET: Employee/5
        public JsonResult Edit(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.User_id == id)
                .SingleOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        // GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Edit/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public ActionResult Edit(int? id)
        //{        
        //    Employee employee = db.Employees.Find(id);

        //    return View(employee);
        //}

        // POST: api/Employee
        //[System.Web.Http.Route("api/Employee/Edit")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
        //    "Sex,MartialStatus")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //    }

        //    return View(employee);
        //}
        //----------------------------------------------------------------------------------------

        // GET: Employee/5
        public JsonResult Detail(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.User_id == id)
                .FirstOrDefault();

            return Json(new { data = "success" }, "application/Javascript", JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Detail/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee Detail(int? id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}
        //----------------------------------------------------------------------------------------




        //Stopping point        

        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        [System.Web.Http.Route("api/Employee/Insurance/{User_id:int}")]
        [System.Web.Http.HttpGet]
        public Employee Insurance(int? id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }

        //GET: api/Employee/5
        [System.Web.Http.Route("api/Employee/EditGroupHealth/{User_id:int}")]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroupHealth(int? id)
        {
            Employee employee = db.Employees.Find(id);

            return View(employee);

        }

       //POST: api/Employee
       [System.Web.Http.Route("api/Employee/EditGroupHealth")]
       [System.Web.Http.HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult EditGroupHealth([Bind(Include = " " +
            " ")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return View(employee);
        }

        //GET: api/Employee/5
        [System.Web.Http.Route("api/Employee/EditLifeInsurance/{User_id:int}")]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLifeInsurance(int? id)
        {
            Employee employee = db.Employees.Find(id);

            return View(employee);

        }

        //POST: api/Employee
       [System.Web.Http.Route("api/Employee/EditLifeInsurance")]
       [System.Web.Http.HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult EditLifeInsurance([Bind(Include = " " +
            " ")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return View(employee);
        }

        //----------------------------------------------------------------------------------------
        // DELETE: api/Employee/5
        [System.Web.Http.Route("api/Employee/Delete/{User_id:int}")]
        [System.Web.Http.HttpGet]
        public void Delete(int? id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();
           
        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void Delete(int id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);
        }

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
