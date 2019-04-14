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
    /// <summary>
    /// This is where I give you all the information about my employees
    /// </summary>
    //[Authorize]
    public class AdminController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> employee = new List<Employee>();
        static List<Family_Info> family = new List<Family_Info>();

        public AdminController()
        {
           
        }

        //public ActionResult Employees()
        //{
        //    return View();
        //}

        //[System.Web.Http.Route("api/Employee/GetEmployees/{Employee_id:int}")]

        //[System.Web.Http.HttpGet]
        //public List<string> GetEmployees(int Employee_id)
        //{
        //    List<string> output = new List<string>();

        //    foreach (var e in employee)
        //    {
        //        output.Add(e.SSN);
        //        output.Add(e.FirstName);
        //        output.Add(e.LastName);
        //        output.Add(e.EmployeeRole);
        //        output.Add(e.JobTitle);
        //        //output.Add(e.isActive);
        //        //output.Add(e.HireDate);
        //    }

        //    return output;
        //}

        //GET: Employees
        //public List<Employee> GetEmployees()
        //{
        //    return db.Employees.ToList();
        //}

        //public JsonResult GetEmployees()
        //{
        //    var output = (from e in db.Employees
        //                  select new
        //                  {
        //                      e.Employee_id,
        //                      e.SSN,
        //                      e.FirstName,
        //                      e.LastName,
        //                      e.EmployeeRole,
        //                      e.JobTitle,
        //                      e.isActive,
        //                      e.HireDate,
        //                      e.EmailAddress,
        //                      e.MailingAddress,
        //                      e.City,
        //                      e.State,
        //                      e.ZipCode,
        //                      e.Department,
        //                      e.AnnualSalary,
        //                      e.EnrollmentType,
        //                      e.Class,
        //                      e.Payroll_id,
        //                      e.WorkStatus,
        //                      e.HoursWorkedPerWeek,

        //                  });

        //    return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        //}

        // GET: api/Employee
        public List<Employee> EmpOverview()
        {
            return employee;
        }

        // GET: api/Employee/5
        public Employee EmpOverview(int Employee_id)
        {
            return employee.Where(e => e.Employee_id == Employee_id).FirstOrDefault();
        }     

        // GET: api/Employee
        public List<Employee> NewEmployeeEnrollment()
        {
            return employee;
        }

        //GET: api/Employee/5
        public Employee EmployeeEnrollmentNew(int id)
        {
            return employee.Where(e => e.Employee_id == id).FirstOrDefault();
        }

        // POST: api/Employee
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EmployeeEnrollmentNew([Bind(Include = "Employee_id,CurrentEmployer,EmployeeRole,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MaritalStatus,JobTitle,HireDate,EffectiveDate,ElgibilityDate,AnnualSalary,HoursWorkedPerWeek")] Employee employee)
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

            //if (employee.MartialStatus == "MarriedWDep")
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
        public Employee EmployeeDetail(int Employee_id)
        {
            return employee.Where(e => e.Employee_id == Employee_id).FirstOrDefault();
        }


        // PUT: api/Employee/5
        //public void UpdateEmployee(int id, Employee value)
        //{
        //    employee[id] = value;
        //}

        // GET: api/Employee/5
        public void EmployeeContact(int? Employee_id)
        {
            Employee employee = db.Employees.Find(Employee_id);
        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EmployeeContact([Bind(Include = "Employee_id,MailingAddress,PhysicalAddress,City,State,ZipCode,County,CityLimits,EmailAddress,PhoneNumber," +
            "CellPhone")] Employee contact)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(contact);
                db.SaveChanges();

            }
    
        }

        // GET: api/Employee/5
        public void EditEmployee(int? Employee_id)
        {
            Employee employee = db.Employees.Find(Employee_id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditEmployee([Bind(Include = "Employee_id,CurrentEmployer,EmployeeRole,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MaritalStatus,JobTitle,HireDate,EffectiveDate,ElgibilityDate,AnnualSalary,HoursWorkedPerWeek,MailingAddress,PhysicalAddress," +
            "City,State,ZipCode,County,CityLimits,EmailAddress,PhoneNumber,CellPhone")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: api/FamilyMember/5
        public void EditSpouse(int? FamilyMember_id)
        {
            Family_Info fmember = db.Family_Info.Find(FamilyMember_id);

        }

        // POST: api/FamilyMember/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditSpouse([Bind(Include = "Employee_id,FamilyMember_id,CurrentEmployer,EmployerAddress,EmployerCity,EmployerState,EmployerZipCode,EmployerPhoneNumber," +
            "EmployeeName,RelationShipToInsured,FirstName,MiddleName,LastName,DateOfBirth,Sex,MaritalStatus,MailingAddress,PhysicalAddress,City,State,ZipCode," +
            "County,CityLimits,EmailAddress,PhoneNumber,CellPhone")] Family_Info fmember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fmember).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: api/FamilyMember/5
        public void EditDependent(int? FamilyMember_id)
        {
            Family_Info fmember = db.Family_Info.Find(FamilyMember_id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditDependent([Bind(Include = "Employee_id,FamilyMember_id,RelationshipToInsured,EmployeeRole,EmployeeName,FirstName,MiddleName,LastName," +
            "DateOfBirth,Sex")] Family_Info fmember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fmember).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE: api/Employee/5
        public void DeleteEmployee(int? Employee_id)
        {
            Employee employee = db.Employees.Find(Employee_id);

            db.Employees.Remove(employee);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public void DeleteEmployee(int Employee_id)
        {
            Employee employee = db.Employees.Find(Employee_id);

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