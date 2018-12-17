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

        public ActionResult GroupHealthInsurance()
        {
            return View(db.Group_Health.ToList());
        }

        public ActionResult GroupHealthEnrollment()
        {
            return View();
        }

        public JsonResult GetGroupHealthInsurance()
        {
            var output = (from g in db.Group_Health
                          select new
                          {
                              g.Employee,
                              g.GroupHealthInsurance_id,
                              g.Employee_id,
                              g.InsuranceCarrier,                            
                              g.PolicyNumber,
                              g.GroupName,
                              g.IMSGroupNumber,
                              g.PhoneNumber,
                              g.ReasonForGrpCoverageRefusal,
                              g.OtherCoverage,
                              g.OtherReason,
                              g.Myself,
                              g.Spouse,
                              g.Dependent,
                              g.OtherInsuranceCoverage,
                              g.CafeteriaPlanYear,
                              g.NoMedicalPlan,
                              g.EmployeeOnly,
                              g.EmployeeAndSpouse,
                              g.EmployeeAndDependent,
                              g.EmployeeAndFamily,                            
                              g.EmployeeSignature,
                              g.EmployeeSignatureDate,
                              g.EmployeeInitials,
                              g.OtherSignature,
                              g.OtherSignatureDate,
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GroupHealthEnrollmentUpdate(int? id)
        {
            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == id)
                .Where(i => i.GroupHealthInsurance_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GroupHealthInsurance(int id)
        //{
        //    Group_Health healthIns = db.Group_Health.Find(id);
        //    return View(healthIns);
        //}

        //public JsonResult GroupHealthInsuranceUpdate(int? id)
        //{
        //    Employee e = db.Employees
        //        .Where(i => i.Employee_id == id)
        //        .SingleOrDefault();

        //    db.Employees.Add(e);
        //    db.SaveChanges();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);

        //}
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
        //----------------------------------------------------------------------------------------


        public ActionResult EditGroupHealthInsurance(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health groupHealth = db.Group_Health.Find(id);
            if (groupHealth == null)
            {
                return HttpNotFound();
            }

            return View(groupHealth);
        }

        public JsonResult GetGroupHealthInsuranceEdit()
        {
            var output = (from g in db.Group_Health
                          select new
                          {
                              g.GroupHealthInsurance_id,
                              g.Employee_id,                        
                              g.InsuranceCarrier,                             
                              g.PolicyNumber,
                              g.GroupName,
                              g.IMSGroupNumber,
                              g.PhoneNumber,
                              g.ReasonForGrpCoverageRefusal,
                              g.OtherCoverage,
                              g.OtherReason,
                              g.Myself,
                              g.Spouse,
                              g.Dependent,
                              g.OtherInsuranceCoverage,
                              g.CafeteriaPlanYear,
                              g.NoMedicalPlan,
                              g.EmployeeOnly,
                              g.EmployeeAndSpouse,
                              g.EmployeeAndDependent,
                              g.EmployeeAndFamily,
                              g.EmployeeSignature,
                              g.EmployeeSignatureDate,
                              g.EmployeeInitials,
                              g.OtherSignature,
                              g.OtherSignatureDate,
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GroupHealthInsuranceUpdate(int? id)
        {
            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == id)
                .Where(i => i.GroupHealthInsurance_id == id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(groupHealth).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
           
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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
            Group_Health groupHealth = db.Group_Health.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(groupHealth);
        }        
    }
}
