using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;

//ask if i need admin and employee model since i created 
//two separate controllers or can i just use one model- employee

namespace Watson.Controllers
{
    /// <summary>
    /// This is where I give you all the information about my employees
    /// </summary>
    //[Authorize]
    public class AdminController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> employee = new List<Employee>();

        public AdminController()
        {
            employee.Add(new Employee { SSN = "0001", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "Admin", JobTitle = "Analyst", User_id = 1 });
            employee.Add(new Employee { SSN = "0002", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "Admin", JobTitle = "HR Manager", User_id = 2 });
            employee.Add(new Employee { SSN = "0003", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "Admin", JobTitle = "HR Manager", User_id = 3 });
        }

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

        //GET: Employees
        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        //public JsonResult EmployeeOverview()
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
        public List<Employee> EmployeeOverview()
        {
            return employee;
        }

        // GET: api/Employee/5
        public Employee EmployeeOverview(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }

        //public JsonResult EmployeeOverview(int id)
        //{
        //    Employee e = db.Employees
        //        .Where(i => i.User_id == id)
        //        .SingleOrDefault();

        //    return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        //}

      

        // GET: api/Employee
        public List<Employee> CreateEmployee()
        {
            return employee;
        }

        //GET: api/Employee/5
        public Employee CreateEmployee(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }

        // POST: api/Employee
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void CreateEmployee([Bind(Include = "User_id,CurrentEmployer,EmployeeRole,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MartialStatus,JobTitle,HireDate,EffectiveDate,ElgibilityDate,AnnualSalary,HoursWorkedPerWeek")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();

            }


            // POST: api/Employee/5
            //public void CreateEmployee(Employee employee)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Employees.Add(employee);
            //    }

            //    db.Employees.Add(employee);

            //    db.SaveChanges();

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

        //GET: api/Employee/5
        public Employee EmployeeDetail(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }


        // PUT: api/Employee/5
        //public void UpdateEmployee(int id, Employee value)
        //{
        //    employee[id] = value;
        //}

        // GET: api/Employee/5
        public void EmployeeContact(int? id)
        {
            Employee employee = db.Employees.Find(id);
        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EmployeeContact([Bind(Include = "User_id,MailingAddress,PhysicalAddress,City,State,ZipCode,County,CityLimits,EmailAddress,PhoneNumber," +
            "CellPhone")] Employee contact)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(contact);
                db.SaveChanges();

            }
    
        }

        // GET: api/Employee/5
        public void EditEmployee(int? id)
        {
            Employee employee = db.Employees.Find(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditEmployee([Bind(Include = "User_id,CurrentEmployer,EmployeeRole,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MartialStatus,JobTitle,HireDate,EffectiveDate,ElgibilityDate,AnnualSalary,HoursWorkedPerWeek,MailingAddress,PhysicalAddress," +
            "City,State,ZipCode,County,CityLimits,EmailAddress,PhoneNumber,CellPhone")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: api/FamilyMember/5
        public void EditSpouse(int? id)
        {
            Family_Info fmember = db.Family_Infoes.Find(id);

        }

        // POST: api/FamilyMember/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditSpouse([Bind(Include = "User_id,FamilyMember_id,CurrentEmployer,EmployerAddress,EmployerCity,EmployerState,EmployerZipCode,EmployerPhoneNumber," +
            "EmployeeName,RelationShipToInsured,FirstName,MiddleName,LastName,DateOfBirth,Sex,MartialStatus,MailingAddress,PhysicalAddress,City,State,ZipCode," +
            "County,CityLimits,EmailAddress,PhoneNumber,CellPhone")] Family_Info fmember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: api/FamilyMember/5
        public void EditDependent(int? id)
        {
            Family_Info fmember = db.Family_Infoes.Find(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditDependent([Bind(Include = "User_id,FamilyMember_id,RelationshipToInsured,EmployeeRole,EmployeeName,FirstName,MiddleName,LastName," +
            "DateOfBirth,Sex")] Family_Info fmember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE: api/Employee/5
        public void DeleteEmployee(int? id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public void DeleteEmployee(int id)
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