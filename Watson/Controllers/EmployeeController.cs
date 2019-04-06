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
        
        private static List<Employee> employee = new List<Employee>();
        
        public EmployeeController()
        {
     
        }

        public ActionResult EmpOverview(Employee employee)
        {
            Employee e = db.Employees.Find(employee);

            e = employee;

            return View(employee);

        }

        public JsonResult GetEmployee(int Employee_id, string EmpNumber, string EmpFirstName, string EmpLastName, 
            string JobTitle, string MailingAddress, string City, string State, string ZipCode)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.SSN = EmpNumber;
            e.FirstName = EmpFirstName;
            e.LastName = EmpLastName;
            e.JobTitle = JobTitle;
            e.MailingAddress = MailingAddress;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;

            int result = e.Employee_id;

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);

        }

        //----------------------------------------------------------------------------------------

        public ActionResult EnrollmentSelection()
        {
            return View();
        }

        public ActionResult NewEmployeeEnrollment()
        {
            return View();
        }
        
        public JsonResult EmployeeEnrollmentNew(string EmployeeRole, string CurrentEmployer, 
            string JobTitle, string EmpNumber, string MaritalStatus, string FirstName, string LastName,
            DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

            e.EmployeeRole = EmployeeRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            e.MaritalStatus = MaritalStatus;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;

            //if (ModelState.IsValid)
            //{
            //    db.Employees.Add(e);

            //    try
            //    {
            //        db.SaveChanges();

            //        if (e.MaritalStatus == "Married")
            //        {
            //            RedirectToAction("SpEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
            //        }
            //        else if (e.MaritalStatus = "MarriedwDep")
            //        {
            //            RedirectToAction("SpEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
            //        }
            //        else if (e.MaritalStatus = "SinglewDep")
            //        {
            //            RedirectToAction("DepEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
            //        }
            //        else
            //        {
            //            RedirectToAction("EmpOverview", "Employee");
            //        }
            //    }

            //    catch (Exception emp)
            //    {
            //        Console.WriteLine(emp);
            //    }
            //}

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);
        }

        //---------------------------------------------------------------------------------------- 

        public ActionResult EmpContact()
        {
            return View();
        }

        public JsonResult EmpEnrollmentContact(int Employee_id, string MailingAddress, string PObox, string City,
            string State, string ZipCode, string County, string PhysicalAddress, string PObox2, string City2,
            string State2, string ZipCode2, string County2, bool CityLimits, string EmailAddress, string PhoneNumber,
            string CellPhone)
        {
            Employee e = new Employee();
                                
            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.PhysicalAddress = PhysicalAddress;
            e.PObox = PObox2;
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;
            e.County = County2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);
                          
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EditEmp(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        public JsonResult EmployeeEditUpdate(int Employee_id, string CurrentEmployer, string JobTitle, string EmployeeNumber,
            string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MaritalStatus, 
            string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string PObox2, string City2, string State2, string ZipCode2, 
            string County2, bool CityLimits, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();
         
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmployeeNumber;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;
            e.MaritalStatus = MaritalStatus;
            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.PhysicalAddress = PhysicalAddress;
            e.PObox = PObox2;
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;
            e.County = County2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            int result = Employee_id;

            if (ModelState.IsValid)
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview");
            }


            return Json(new { data = e }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EmpDetail(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        public JsonResult GetEmpDetail(int Employee_id)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);    
        }

        //----------------------------------------------------------------------------------------

        public ActionResult DeleteEmp(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(Employee_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Employee_id)
        {
            Employee employee = db.Employees.Find(Employee_id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(Employee_id);

            return RedirectToAction("EmpOverview");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //----------------------------------------------------------------------------------------


       
        
    }
}
