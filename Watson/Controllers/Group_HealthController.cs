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

        public JsonResult GetGroupHealthInsurance(int groupHealth_id, int e_id)
        {
            var output = (from g in db.Group_Health
                          select new
                          {
                              g.GroupHealthInsurance_id,
                              g.Employee_id,
                              g.Employee,
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

        public JsonResult GroupHealthEnrollmentUpdate(int groupHealth_id, int e_id)
        {
            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == e_id)
                .Where(i => i.GroupHealthInsurance_id == groupHealth_id)
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

        public ActionResult EditGroupHealth(int? id)
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

        public ActionResult GroupHealthDetail(int? id)
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

        public ActionResult HealthInsurancePremiums(int id)
        {
            InsurancePremium insurancePremium = db.InsurancePremiums.Find(id);
            return View(insurancePremium);
        }

        public JsonResult GetHealthInsurancePremium()
        {
            var output = (from insurancePremium in db.InsurancePremiums
                          select new
                          {
                              insurancePremium.InsurancePremium_id,
                              insurancePremium.InsurancePlan_id,
                              insurancePremium.EmployeeOnly,
                              insurancePremium.EmployeeAndSpouse,
                              insurancePremium.EmployeeAndDependent,
                              insurancePremium.EmployeeAndFamily,
                              insurancePremium.YearlyPremiumCost,                  
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HealthInsurancePremiumUpdate(int? id)
        {
            InsurancePremium insurancePremium = db.InsurancePremiums
                .Where(i => i.InsurancePremium_id == id)
                .Where(i=> i.InsurancePlan_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //Not sure whether i use db.InsPremium, db.InsPlan, db.InsPlanDetails or all three??
        public ActionResult HealthInsuranceSupplement(int id)
        {
            InsurancePlanDetail insSupplement = db.InsurancePlanDetails.Find(id);
            return View(insSupplement);
        }

        public JsonResult GetHealthInsuranceSupplement()
        {
            var output = (from insPlandetail in db.InsurancePlanDetails
                          select new
                          {
                              insPlandetail.InsurancePlanDetail_id,
                              insPlandetail.Item,
                              insPlandetail.Detail,

                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HealthInsuranceSupplementUpdate(int? id)
        {
            InsurancePlanDetail insPlandetail = db.InsurancePlanDetails
                .Where(i => i.InsurancePlanDetail_id == id)
                .Where(i => i.InsurancePlan_id == id)
                .SingleOrDefault();

            return Json(new { data = "success}" }, JsonRequestBehavior.AllowGet);
        }

        // not sure whether I use both db.Deductions and db.InsurancePlan or just db.Deductions
        //need to add employee signature and signature date to db.Deductions table and change data types
        public ActionResult SalaryRedirectionAgreement()
        {
            return View();
        }

        
        public JsonResult GetSalaryRedirectionAgreement()
        {
            var output = (from d in db.Deductions
                          select new
                          {
                              d.Deductions_id,
                              d.Employee_id,
                              d.Coverage,
                              d.Provider,
                              d.EEelectionPreTax,
                              d.PremiumPreTax,
                              d.EEelectionPostTax,
                              d.PremiumPostTax,
                              d.TotalPreTax,
                              d.TotalPostTax,
                          });

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalaryRedirectionAgreementUpdate(int id)
        {
            Deduction deduction = db.Deductions
                .Where(i => i.Deductions_id == id)
                .Where(i => i.Employee_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteGroupHealth(int? id)
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

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteGroupHealth")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroupHealth(int id)
        {
            Group_Health groupHealth = db.Group_Health.Find(id);
            db.Group_Health.Remove(groupHealth);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);

            return RedirectToAction("GroupHealthEnrollment");
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
