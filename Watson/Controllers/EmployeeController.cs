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
    public class EmployeeController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> employee = new List<Employee>();

        public EmployeeController()
        {
            employee.Add(new Employee { SSN = "0001", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "Admin", JobTitle = "", User_id = 1 });
            employee.Add(new Employee { SSN = "", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "Admin", JobTitle = "", User_id = 2 });
            employee.Add(new Employee { SSN = "", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "Admin", JobTitle = "", User_id = 3 });
        }


        // GET: api/Employee
        public List<Employee> GetEmployee()
        {
            return employee;
        }

        // GET: api/Employee/5
        public Employee GetEmployee(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }

        //public JsonResult GetEmployee(int id)
        //{
        //    Employee e = db.Employees
        //        .Where(i => i.User_id == id)
        //        .SingleOrDefault();

        //    return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        //}

        // GET: Employees
        //public List<Employee> GetEmployees()
        //{
        //    return db.Employees.ToList();
        //}

        // GET: api/Employee
        public List<Employee> EmployeeEnrollment()
        {
            return employee;
        }

        //GET: api/Employee/5
        public Employee EmployeeEnrollment(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }

        // POST: api/Employee
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EmployeeEnrollment([Bind(Include = "User_id,CurrentEmployer,EmployeeRole,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MartialStatus")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }

        }

        //GET: api/Employee/5
        public Employee Detail(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }


        // PUT: api/Employee/5
        //public void UpdateEmployee(int id, Employee value)
        //{
        //    employee[id] = value;
        //}

        // GET: api/Employee/5
        public void Contact(int? id)
        {
            Employee employee = db.Employees.Find(id);
        }

        // POST: api/Employee/5
        public void Contact([Bind(Include = "User_id,MailingAddress,PhysicalAddress,City,State,ZipCode,County,CityLimits,EmailAddress" +
            "PhoneNumber,CellPhone")] Employee contact)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(contact);
                db.SaveChanges();
            }
        }
            // GET: api/Employee/5
        public void Edit(int? id)
        {
            Employee employee = db.Employees.Find(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit([Bind(Include = "User_id,CurrentEmployer,EmployeeRole,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MartialStatus")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE: api/Employee/5
        public void Delete(int? id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);
           
        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void Delete(int id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();

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
