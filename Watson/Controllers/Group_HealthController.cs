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

        public ActionResult GrpHealthInsPremiums()
        {
            return View();
        }

        public JsonResult GrpHealthInsPremiumUpdate(string EmpOnly, string EmpAndSp, string EmpAndDep, string EmpAndFamily, decimal YearlyPremiumCost,
            string InsMECPlan, string InsStndPlan, string InsBuyUpPlan, string DentalPlan, string VisionPlan)
        {
            InsurancePlan insPlan = new InsurancePlan();

            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            InsurancePremium insPremium = new InsurancePremium();

            insPremium.YearlyPremiumCost = YearlyPremiumCost;
            insPremium.EmployeeOnly = EmpOnly;
            insPremium.EmployeeAndSpouse = EmpAndSp;
            insPremium.EmployeeAndDependent = EmpAndDep;
            insPremium.EmployeeAndFamily = EmpAndFamily;
            insPremium.YearlyPremiumCost = YearlyPremiumCost;

            db.InsurancePlans.Add(insPlan);
            db.InsurancePremiums.Add(insPremium);
            db.SaveChanges();

            return Json(new { data = insPlan, insPremium }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsSupplement()
        {
            return View();
        }

        public JsonResult GrpHealthInsSupplementUpdate(int InsPlanDetail_id, string Item, string Detail)
        {
            InsurancePlanDetail insDetail = new InsurancePlanDetail();

            insDetail.Item = Item;
            insDetail.Detail = Detail;

            return Json(new { data = insDetail }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthEnrollment()
        {
            return View();
        }

        public JsonResult GrpHealthEnrollmentNew(string InsuranceCarrier, string PolicyNumber, string GroupName, string IMSGroupNumber,
            string PhoneNumber, string ReasonForGrpCoverageRefusal, bool OtherCoverage, bool OtherReason, bool Myself, bool Spouse,
            bool Dependent, string OtherInsuranceCoverage, DateTime CafeteriaPlanYear, bool NoMedicalPlan, bool EmployeeOnly, 
            bool EmployeeAndSpouse, bool EmployeeAndDependent, bool EmployeeAndFamily, string EmployeeSignature, 
            DateTime EmployeeSignatureDate, string EmployeeInitials, string OtherSignature, DateTime OtherSignatureDate)
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

        public ActionResult EditGrpHealthIns(int? GrpHealthIns_id)
        {
            if (GrpHealthIns_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(GrpHealthIns_id);
            if (g == null)
            {
                return HttpNotFound();
            }

            return View(g);
        }

        public JsonResult GrpHealthInsEditUpdate(int GrpHealthIns_id, string InsuranceCarrier, string PolicyNumber, string GroupName,
            string IMSGroupNumber, string PhoneNumber, string ReasonForGrpCoverageRefusal, bool OtherCoverage,
            bool OtherReason, bool Myself, bool Spouse, bool Dependent, string OtherInsuranceCoverage,
            DateTime CafeteriaPlanYear, bool NoMedicalPlan, bool EmployeeOnly, bool EmployeeAndSpouse,
            bool EmployeeAndDependent, bool EmployeeAndFamily, string EmployeeSignature, DateTime EmployeeSignatureDate,
            string EmployeeInitials, string OtherSignature, DateTime OtherSignatureDate)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GrpHealthIns_id)
                .Single();

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

            if (ModelState.IsValid)
            {
                db.Entry(g).State = System.Data.Entity.EntityState.Modified;

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

            return Json(new { data = g }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsDetail(int? GrpHealthIns_id)
        {
            if (GrpHealthIns_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group_Health g = db.Group_Health.Find(GrpHealthIns_id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        
        //----------------------------------------------------------------------------------------
        // use both db.Deductions and db.InsurancePlan or just db.Deductions?
        //need to add employee signature and signature date to db.Deductions table and change data types
        public ActionResult SalaryRedirectAgreement()
        {
            return View();
        }

        
        public JsonResult SalaryRedirectionUpdate(int Deductions_id, string Coverage, string Provider, string EElectionPreTax, decimal PremiumPreTax,
            string EElectionPostTax, decimal PremiumPostTax, decimal TotalPreTax, decimal TotalPostTax)
        {
            Deduction d = new Deduction();

            d.Coverage = Coverage;
            d.Provider = Provider;
            d.EEelectionPreTax = EElectionPreTax;
            d.PremiumPreTax = PremiumPreTax;
            d.EEelectionPostTax = EElectionPostTax;
            d.PremiumPostTax = PremiumPostTax;
            d.TotalPreTax = TotalPreTax;
            d.TotalPostTax = TotalPostTax;

            db.Deductions.Add(d);
            db.SaveChanges();

            return Json(new { data = d }, JsonRequestBehavior.AllowGet);
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
