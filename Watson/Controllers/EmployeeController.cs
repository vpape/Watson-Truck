﻿using System;
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

        public ActionResult EmployeeOverview(int id)
        {
            Employee employee = db.Employees.Find(id);
            //Family_Info family = db.Family_Info.Find(id);

            return View(employee);

            //return View(db.Employees.ToList());
        }

        public JsonResult GetEmployee()
        {
            var output = (from e in db.Employees
                          select new
                          {
                             e.Employee_id,
                             e.SSN,
                             e.FirstName,
                             e.LastName,
                             e.JobTitle,
                             e.EmailAddress,
                             e.MailingAddress,
                             e.City,
                             e.State,
                             e.ZipCode,

                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeUpdate(int id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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

        public ActionResult EnrollmentSelection()
        {
            return View();
        }

        public ActionResult EmployeeEnrollment(int id)
        {
            Employee employee = db.Employees.Find(id);

            return View(employee);
        }

        public JsonResult GetEmployeeEnrollment()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Sex,
                              e.MaritalStatus,
                          });

            //Redirect is based on marital status, which it's not working
            //if (e.MaritalStatus == "Married")
            //{
            //    return RedirectToAction("SpouseEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
            //}
            //else if (e.MaritalStatus == "MarriedwDep")
            //{
            //    return RedirectToAction("SpouseEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
            //}
            //else if (e.MaritalStatus == "SinglewDep")
            //{
            //    return RedirectToAction("DependentEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
            //}

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }
        
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeEnrollmentUpdate(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Employees.Add(e);
                db.SaveChanges();
            }
            
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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
        //----------------------------------------------------------------------------------------

        public ActionResult Contact(int id)
        {
            Employee employee = db.Employees.Find(id);

            return View(employee);
        }

        public JsonResult Contact()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.Employee_id,
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

        [ValidateAntiForgeryToken]
        public JsonResult Contact(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Employees.Add(e);
                db.SaveChanges();
            }

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult Edit()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Sex,
                              e.MaritalStatus,
                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
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

        public JsonResult Detail(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .FirstOrDefault();

            return Json(new { data = "success" }, "application/Javascript", JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Detail(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee e = db.Employees.Find(id);
        //    if (e == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(e);
        //}

        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Detail/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee Detail(int? id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}
        //----------------------------------------------------------------------------------------

        public JsonResult EmployeeInsurance()
        {
            var output = (from e in db.Employees
                          select new
                          {

                              
                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);

        }

        public JsonResult EmployeeInsurance(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);

        }

        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Insurance/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee Insurance(int? id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}

        // POST: api/Employee
        //[System.Web.Http.Route("api/Employee/Insurance")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Insurance([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
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

        public JsonResult LifeInsuranceEnrollemnt()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.Employee_id
                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        public JsonResult LifeInsuranceEnrollment(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditLifeInsurance()
        {
            var output = (from e in db.Employees
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Sex,
                              e.MaritalStatus,
                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditLifeInsurance(int? id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        // [System.Web.Http.Route("api/Employee/EditLifeInsurance/{User_id:int}")]
        // [System.Web.Http.HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult EditLifeInsurance(int? id)
        // {
        //     Employee employee = db.Employees.Find(id);

        //     return View(employee);

        // }

        // //POST: api/Employee
        //[System.Web.Http.Route("api/Employee/EditLifeInsurance")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        // public ActionResult EditLifeInsurance([Bind(Include = " " +
        //     " ")] Employee employee)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        //         db.SaveChanges();
        //     }

        //     return View(employee);
        // }
        //----------------------------------------------------------------------------------------






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


        // DELETE: api/Employee/5
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
