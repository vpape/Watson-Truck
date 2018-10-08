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
            employee.Add(new Employee { SSN = "", FirstName = "", LastName = "", EmployeeRole = "", JobTitle = "", User_id = 1});
            employee.Add(new Employee { SSN = "", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "", JobTitle = "", User_id = 2 });
            employee.Add(new Employee { SSN = "", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "", JobTitle = "", User_id = 3 });
            employee.Add(new Employee { SSN = "", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "", JobTitle = "", User_id = 4 });
        }

        //public EmployeeController(string SSN, string FirstName, string LastName, string EmployeeRole, string JobTitle, int id)
        //{
        //    employee.Add(new Employee { SSN = "", FirstName = "", LastName = "", EmployeeRole = "", JobTitle = "", User_id = id });
        //}


        /// <summary>
        /// Gets a list of the employees
        /// </summary>
        /// <param name="User_id">The unique identifier for this person</param>
        /// <param name="SSN">We want to know their employee#</param>
        /// <returns>A list of employees Emp#, FN, LN, EmpRole, & JobTitle</returns>
        //[Route("api/Employee/GetEmployees/{User_id:int}/{SSN:string}")]
        //[HttpGet]
        public List<string> GetEmployees(int User_id, string SSN)
        {
            List<string> output = new List<string>();

            foreach (var e in employee)
            {
                output.Add(e.SSN);
                output.Add(e.FirstName);
                output.Add(e.LastName);
                output.Add(e.EmployeeRole);
                output.Add(e.JobTitle);
                //output.Add(e.isActive);
            }

            return output;
        }

        //public JsonResult GetEmployees()
        //{
        //    var output = (from e in db.Employees
        //                  select new
        //                  {
        //                      e.User_id,
        //                      e.SSN,
        //                      e.FirstName,
        //                      e.LastName,
        //                      e.EmployeeRole,
        //                      e.JobTitle,
        //                      e.isActive
        //                  });

        //    return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        //}

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
        public List<Employee> Index()
        {
            return db.Employees.ToList();
        }

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
        public void Edit(int? id)
        {
            Employee employee = db.Employees.Find(id);

        }

        // DELETE: api/Employee/5
        public void Delete(int? id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);
           
        }

        //---------------POST Methods---------------//


        // POST: api/Employee/5
        public void EmployeeEnrollment(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
            }

            db.Employees.Add(employee);

            db.SaveChanges();

            //if (employee.MartialStatus == "MarriedwDep")
            //{
            //    RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.User_id, employee.MartialStatus });
            //}
            //else if (employee.MartialStatus == "SingleWDep")
            //{
            //    RedirectToRouteResult("DependentEnrollment", "Family_Info", new {employee.User_id, employee.MartialStatus});
            //}
            //else
            //{
            //    RedirectToRouteResult("Index", "Employee");
            //}

        }

        // POST: api/Employee/5
        public void Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }

        // POST: api/Employee/5
        public void Contact(Employee contact)
        {
            employee.Add(contact);
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
