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

        //private static Group_Health groupHealth = new Group_Health();
        private static List<Group_Health> group = new List<Group_Health>();

        public Group_HealthController()
        {

        }

        //public ActionResult GroupHealthInsurance(Group_Health group)
        //{
        //    Group_Health grp = db.Group_Health.Find(group);

        //    grp = group;

        //    return View(group);
        //}

        public ActionResult GrpHealthEnrollment()
        {
            return View();
        }

        public JsonResult GrpHealthEnrollmentNew(string InsuranceCarrier, string PolicyNumber, string GroupName,
            string IMSGroupNumber, string PhoneNumber, string ReasonForGrpCoverageRefusal, bool OtherCoverage, 
            bool OtherReason, bool Myself, bool Spouse, bool Dependent, string OtherInsuranceCoverage,
            DateTime CafeteriaPlanYear, bool NoMedicalPlan, bool EmployeeOnly, bool EmployeeAndSpouse,
            bool EmployeeAndDependent, bool EmployeeAndFamily, string EmployeeSignature,DateTime EmployeeSignatureDate,
            string EmployeeInitials, string OtherSignature, DateTime OtherSignatureDate)
        {
            Group_Health g = new Group_Health();

            g.InsuranceCarrier = InsuranceCarrier;
            g.PolicyNumber = PolicyNumber;
            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;
            g.PhoneNumber = PhoneNumber;
            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.OtherCoverage = OtherCoverage;
            g.OtherReason = OtherReason;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            g.OtherInsuranceCoverage = OtherInsuranceCoverage;
            g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoMedicalPlan;
            g.EmployeeOnly = EmployeeOnly;
            g.EmployeeAndSpouse = EmployeeAndSpouse;
            g.EmployeeAndDependent = EmployeeAndDependent;
            g.EmployeeAndFamily = EmployeeAndFamily;
            g.EmployeeSignature = EmployeeSignature;
            g.EmployeeSignatureDate = EmployeeSignatureDate;
            g.EmployeeInitials = EmployeeInitials;
            g.OtherSignature = OtherSignature;
            g.OtherSignatureDate = OtherSignatureDate;

            db.Group_Health.Add(g);
            db.SaveChanges();

            return Json(new { data = g }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EditGrpHealthIns(int? id)
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

        public JsonResult GroupHealthInsEditUpdate()
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

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsDetail(int? id)
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

        //----------------------------------------------------------------------------------------
        public ActionResult GrpHealthInsPremiums()
        {
            return View();
        }

        public JsonResult HealthInsPremiumUpdate()
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

        //----------------------------------------------------------------------------------------
  
        public ActionResult GrpHealthInsSupplement()
        {
            //InsurancePlanDetail insSupplement = db.InsurancePlanDetails.Find(id);
            return View();
        }

        public JsonResult HealthInsSupplementUpdate()
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

        //----------------------------------------------------------------------------------------
        // use both db.Deductions and db.InsurancePlan or just db.Deductions?
        //need to add employee signature and signature date to db.Deductions table and change data types
        public ActionResult SalaryRedirectAgreement()
        {
            return View();
        }

        
        public JsonResult GetSalaryRedirectAgreement()
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

        //----------------------------------------------------------------------------------------

        public ActionResult DeleteGrpHealthIns(int? id)
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

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteGrpHealthIns")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroupHealth(int id)
        {
            Group_Health groupHealth = db.Group_Health.Find(id);
            db.Group_Health.Remove(groupHealth);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);

            return RedirectToAction("GrpHealthEnrollment");
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


        public ActionResult LifeInsuranceEnrollment()
        {
            return View();
        }

        public JsonResult LifeInsEnrollmentNew(int lifeIns_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                         {
                             e.LifeInsurance_id,
                             e.Employee_id,
                             e.GroupPlanNumber,
                             e.BenefitsEffectiveDate,
                             e.InitialEnrollment,
                             e.ReEnrollment,
                             e.AddEmployeeAndDependents,
                             e.DropRefuseCoverage,
                             e.InformationChange,
                             e.IncreaseAmount,
                             e.FamilyStatusChange,
                             e.MarriedOrHaveSpouse,
                             e.HaveChildrenOrHaveDependents,
                             e.DateOfMarriage,
                             e.PlacementDateOfAdoptedChild,
                             e.AddDependent,
                             e.DropDependent,
                             e.Student,
                             e.Disabled,
                             e.NonStandardDependent,
                             e.DropEmployee,
                             e.DropSpouse,
                             e.DropDependents,
                             e.LastDayOfCoverage,
                             e.TerminationOfEmployment,
                             e.Retirement,
                             e.LastDayWorked,
                             e.OtherEvent,
                             e.OtherEventDate,
                             e.EmployeeDentalDrop,
                             e.SpouseDentalDrop,
                             e.DependentDentalDrop,
                             e.EmployeeVisionDrop,
                             e.SpouseVisionDrop,
                             e.DependentVisionDrop,
                             e.DropBasicLife,
                             e.DropDental,
                             e.DropVision,
                             e.TerminationOfEmploymentDate,
                             e.Divorce,
                             e.DivorceDate,
                             e.DeathOfSpouse,
                             e.DeathOfSpouseDate,
                             e.TerminationOrExpirationOfCoverage,
                             e.TerminationOrExpirationOfCoverageDate,
                             e.DentalCoverageLost,
                             e.VisionCoverageLost,
                             e.CoveredUnderOtherInsurance,
                             e.CoveredUnderOtherInsReason,
                             e.EmployeeOnly,
                             e.EmployeeAndSpouse,
                             e.EmployeeAndDependent,
                             e.EmployeeAndFamily,
                             e.DoNotWantDentalCoverage,
                             e.EmployeeCoveredUnderOtherDentalPlan,
                             e.SpouseCoveredUnderOtherDentalPlan,
                             e.DependentsCoveredUnderOtherDentalPlan,
                             e.DoNotWantVisionCoverage,
                             e.EmployeeCoveredUnderOtherVisionPlan,
                             e.SpouseCoveredUnderOtherVisionPlan,
                             e.DependentsCoveredUnderOtherVisionPlan,
                             e.OwnerBasicLifeWithADandDPolicyAmount,
                             e.ManagerBasicLifeWithADandDPolicyAmount,
                             e.EmployeeBasicLifeWithADandDPolicyAmount,
                             e.SpouseBasicLifeWithADandDPolicyAmount,
                             e.DoNotWantBasicLifeCoverageWithADandD,
                             e.AmountOfPreviousPolicy,
                             e.EmployeeSignature,
                             e.EmployeeSignatureDate
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        public ActionResult EditLifeInsurance(int? lifeIns_id)
        {
            if (lifeIns_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Life_Insurance lifeIns = db.Life_Insurance.Find(lifeIns_id);
            if (lifeIns_id == null)
            {
                return HttpNotFound();
            }

            return View(lifeIns_id);
        }

        public JsonResult EditLifeInsUpdate(int lifeIns_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                         {
                             e.LifeInsurance_id,
                             e.Employee_id,

                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
    }
}
