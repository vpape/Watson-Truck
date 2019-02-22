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

        public ActionResult EmployeeOverview(int e_id)
        {
            Employee emp = db.Employees.Find(e_id);

            emp.Employee_id = e_id;

            //return View(db.Employees.ToList());
            return View(emp);
        }

        public JsonResult GetEmployee(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                             e.Employee_id,
                             e.SSN,
                             e.FirstName,
                             e.LastName,
                             e.JobTitle,
                             e.MailingAddress,
                             e.City,
                             e.State,
                             e.ZipCode,

                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .FirstOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();

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

        public ActionResult EmployeeEnrollment()
        {
            return View();
        }

        public JsonResult GetEmployeeEnrollment(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Gender,
                              e.MaritalStatus,
                          };

      

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }
        
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeEnrollmentUpdate(int e_id)
        {
            Employee employee = db.Employees
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);

                try
                {
                    db.SaveChanges();

                    //Redirect is based on marital status, which it's not working
                    //if (employee.MaritalStatus == "Married")
                    //{
                    //    return RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
                    //}
                    //else if (employee.MaritalStatus == "MarriedwDep")
                    //{
                    //    return RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
                    //}
                    //else if (employee.MaritalStatus == "SinglewDep")
                    //{
                    //    return RedirectToAction("DependentEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
                    //}
                    //else
                    //{
                    //    return RedirectToAction("EmployeeOverview", "Employee");
                    //}
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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

        public ActionResult Contact()
        {
            return View();
        }

        public JsonResult GetEmployeeContact(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
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
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
                          
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ContactUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();
            
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int? e_id)
        {
            //return View();
            if (e_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(e_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public JsonResult GetEmployeeEdit(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Gender,
                              e.MaritalStatus,
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
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeEditUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }            

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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
            
        public ActionResult Detail(int? e_id)
        {
            if (e_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(e_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public JsonResult GetDetail(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Gender,
                              e.MaritalStatus,
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
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);    
        }

        public JsonResult DetailUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .FirstOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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

        public ActionResult EmployeeInsurance()
        {
            return View();
        }

        //Need to finish Group and Life Insurance 
        public ActionResult LifeInsurance(int lifeIns_id, int e_id)
        {
           Employee emp = db.Employees.Find(e_id);
           Life_Insurance lifeIns = db.Life_Insurance.Find(lifeIns_id);

           return View(lifeIns); 
        }

        public JsonResult GetLifeInsurance(int lifeIns_id, int e_id)
        {
            var output = from e in db.Life_Insurance
                          select new
                          {
                              e.Employee_id,
                              e.LifeInsurance_id,
                             
                              
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LifeInsuranceUpdate(int lifeIns_id, int e_id)
        {
            Life_Insurance lifeIns = db.Life_Insurance
                .Where(i => i.LifeInsurance_id == lifeIns_id)
                .SingleOrDefault();

            //Employee e = db.Employees
            //    .Where(i => i.Employee_id == e_id)
            //    .SingleOrDefault();

            db.Life_Insurance.Add(lifeIns);
            //db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLifeInsurance()
        {
            return View();
        }

        public JsonResult GetEditLifeInsurance(int lifeIns_id, int e_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                          {
                              e.Employee_id,
                              e.LifeInsurance_id,
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LifeInsuranceEditUpdate(int lifeIns_id, int e_id)
        {
            Life_Insurance lifeIns = db.Life_Insurance
                .Where(i => i.LifeInsurance_id == lifeIns_id)
                .SingleOrDefault();

            //Employee e = db.Employees
            //    .Where(i => i.Employee_id == e_id)
            //    .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(lifeIns).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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
       
        public ActionResult Delete(int? e_id)
        {
            if (e_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(e_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);           
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int e_id)
        {
            Employee employee = db.Employees.Find(e_id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(e_id);

            return RedirectToAction("EmployeeOverview");
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
