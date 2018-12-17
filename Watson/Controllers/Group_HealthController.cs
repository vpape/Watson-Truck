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
    public class Group_HealthController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static Group_Health groupHealth = new Group_Health();

        public Group_HealthController()
        {
           
        }

        public JsonResult GroupHealthEnrollment()
        {
            var output = (from g in db.Group_Health
                          select new
                          {
                              g.GroupHealthInsurance_id,
                              g.Employee_id,
                              g.InsuranceCarrier,                            
                              g.PolicyNumber,
                              g.EmployeeSignature,
                              g.Employee
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GroupHealthEnrollment(int? id)
        {
            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == id)
                .Where(i => i.GroupHealthInsurance_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        // GET: api/Employee
        //[System.Web.Http.Route("api/Group_Health/GroupHealthEnrollment")]
        //[System.Web.Http.HttpGet]
        //public List<Group_Health> GroupHealthEnrollment()
        //{
        //    return groupHealth;
        //}

        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Group_Health/GroupHealthEnrollment/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Group_Health GroupHealthEnrollment(int id)
        //{
        //    return groupHealth.Where(i => i.User_id == id).FirstOrDefault();
        //}


        // POST: api/Employee
        //[System.Web.Http.Route("api/Group_Health/GroupHealthEnrollment")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GroupHealthEnrollment([Bind(Include = "User_id,CurrentEmployer,JobTitle,SSN,FirstName,LastName,DateOfBirth," +
        //    "Sex,MartialStatus")] Group_Health groupHealth)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Group_Health.Add(groupHealth);
        //        db.SaveChanges();
        //    }

        //    return View(groupHealth);
        //}

        // PUT: api/Employee/5
        //public void UpdateEmployee(int id, Employee value)
        //{
        //    employee[id] = value;
        //}
        //----------------------------------------------------------------------------------------

        public JsonResult EditGroupHealth()
        {
            var output = (from g in db.Group_Health
                          select new
                          {
                              g.GroupHealthInsurance_id,
                              g.Employee_id,                        
                              g.InsuranceCarrier,                             
                              g.PolicyNumber,
                              g.EmployeeSignature,
                              g.Employee

                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditGroupHealth(int? id)
        {
            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == id)
                .Where(i => i.GroupHealthInsurance_id == id)
                .SingleOrDefault();

            db.Group_Health.Add(g);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        // GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/EditGroupHealth/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public ActionResult EditGroupHealth(int? id)
        //{        
        //    Employee employee = db.Employees.Find(id);

        //    return View(employee);
        //}

        // POST: api/Employee
        //[System.Web.Http.Route("api/Employee/EditGroupHealth")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditGroupHealth([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
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

        public ActionResult GroupHealthDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group_Health g = db.Group_Health.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        //public JsonResult GroupHealthDetail(int? id)
        //{
        //    Group_Health g = db.Group_Healths
        //        .Where(i => i.User_id == id)
        //        .Where(i => i.GroupHealthInsurance_id == id)
        //        .FirstOrDefault();

        //    return Json(new { data = "success" }, "application/Javascript", JsonRequestBehavior.AllowGet);
        //}



        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/GroupHealthDetail/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee GroupHealthDetail(int? id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}
        //----------------------------------------------------------------------------------------

        
    }
}
