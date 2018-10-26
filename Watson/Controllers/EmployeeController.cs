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
    public class EmployeeController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> employee = new List<Employee>();

        public EmployeeController()
        {
            employee.Add(new Employee { SSN = "0001", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "Admin", JobTitle = "Analyst", User_id = 1 });
            employee.Add(new Employee { SSN = "0002", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "Admin", JobTitle = "HR Manager", User_id = 2 });
            employee.Add(new Employee { SSN = "0003", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "Admin", JobTitle = "HR Manager", User_id = 3 });
        }

        [System.Web.Http.Route("api/Employee/EmployeeOverview/{User_id:int}/{SSN:string}")]
        [System.Web.Http.HttpGet]
        public List<string> EmployeeOverview(int User_id, string SSN)
        {
            List<string> output = new List<string>();

            foreach (var e in employee)
            {
                output.Add(e.SSN);
                output.Add(e.FirstName);
                output.Add(e.LastName);
                output.Add(e.JobTitle);
                output.Add(e.MailingAddress);
                output.Add(e.City);
                output.Add(e.State);
            }

            return output;
        }

        // GET: api/Employee
        //[System.Web.Http.Route("api/Employee/EmployeeOverview/{User_id:int}/{SSN:string}")]
        [System.Web.Http.Route("api/Employee/EmployeeOverview")]
        [System.Web.Http.HttpGet]
        public List<Employee> EmployeeOverview()
        {
            List<string> output = new List<string>();

            foreach (var e in employee)
            {
                output.Add(e.SSN);
                output.Add(e.FirstName);
                output.Add(e.LastName);
                output.Add(e.JobTitle);
                output.Add(e.MailingAddress);
                output.Add(e.City);
                output.Add(e.State);
            }

            return employee;
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
        //                      e.JobTitle,
        //                  });

        //    return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        //}

        // GET: api/Employee/5
        [System.Web.Http.Route("api/Employee/EmployeeOverview/{User_id:int}")]
        [System.Web.Http.HttpGet]
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
        [System.Web.Http.Route("api/Employee/EmployeeEnrollment")]
        [System.Web.Http.HttpGet]
        public List<Employee> EmployeeEnrollment()
        {
            return employee;
        }

        //GET: api/Employee/5
        [System.Web.Http.Route("api/Employee/EmployeeEnrollment/{User_id:int}")]
        [System.Web.Http.HttpGet]
        public Employee EmployeeEnrollment(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }

        // POST: api/Employee
        [System.Web.Http.Route("api/Employee/EmployeeEnrollment")]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EmployeeEnrollment([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MartialStatus")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }

        }

        //GET: api/Employee/5
        [System.Web.Http.Route("api/Employee/Detail/{User_id:int}")]
        [System.Web.Http.HttpGet]
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
        [System.Web.Http.Route("api/Employee/Contact/{User_id:int}")]
        [System.Web.Http.HttpGet]
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
        [System.Web.Http.Route("api/Employee/Edit/{User_id:int}")]
        [System.Web.Http.HttpGet]
        public void Edit(int? id)
        {
            Employee employee = db.Employees.Find(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.Route("api/Employee/Edit")]
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
            "Sex,MartialStatus")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: api/Employee/5
        public void EditGroupHealth(int? id)
        {
            Employee employee = db.Employees.Find(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditGroupHealth([Bind(Include = " " +
            " ")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: api/Employee/5
        public void EditLifeInsurance(int? id)
        {
            Employee employee = db.Employees.Find(id);

        }

        // POST: api/Employee/5
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void EditLifeInsurance([Bind(Include = " " +
            " ")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Delete/{User_id:int}")]
        //[System.Web.Http.HttpGet]
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
