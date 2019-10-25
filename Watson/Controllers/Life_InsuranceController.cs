using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Dynamic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Watson.Models;
using Watson.ViewModels;

namespace Watson.Controllers
{
    public class Life_InsuranceController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static Group_Health grpHealth = new Group_Health();
        private static Employee employee = new Employee();
        private static List<Family_Info> family = new List<Family_Info>();
        private static Life_Insurance lifeIns = new Life_Insurance();

        public ActionResult LifeInsuranceEnrollment(int? LifeInsurance_id, int? Employee_id, int? GroupHealthInsurance_id)
        {
            ViewBag.LifeInsurance_id = LifeInsurance_id;
            ViewBag.Employee_id = Employee_id;
            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;

            EmployeeAndInsuranceVM empAndInsVM = new EmployeeAndInsuranceVM();

            empAndInsVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            empAndInsVM.lifeIns = db.Life_Insurance.FirstOrDefault(i => i.Employee_id == Employee_id);

            empAndInsVM.spouse = db.Family_Info.FirstOrDefault(i => i.Employee_id == Employee_id && i.RelationshipToInsured == "Spouse");
            empAndInsVM.family = db.Family_Info.Where(i => i.Employee_id == Employee_id && i.RelationshipToInsured != "Spouse").ToList();
            if (empAndInsVM.spouse != null)
            {
                empAndInsVM.spouse = db.Family_Info.FirstOrDefault(i => i.Employee_id == Employee_id && i.FamilyMember_id == empAndInsVM.spouse.FamilyMember_id);
                empAndInsVM.family = db.Family_Info.Where(i => i.Employee_id == Employee_id && i.FamilyMember_id != empAndInsVM.spouse.FamilyMember_id).ToList();
            }
            else
            {
                empAndInsVM.spouse = null;
                empAndInsVM.family = db.Family_Info.Where(i => i.Employee_id == Employee_id).ToList();
            }


            return View(empAndInsVM);
        }

        //Create-LifeIns
        public JsonResult LifeInsEnrollmentNew(int? LifeInsurance_id, int Employee_id, string GroupPlanNumber, DateTime BenefitsEffectiveDate, string InitialEnrollment,
            string ReEnrollment, string AddEmployeeAndDependents, string DropRefuseCoverage, string InformationChange, string IncreaseAmount, string FamilyStatusChange,
            string SubTotalCode, string Married, DateTime DateOfMarriage, string OtherDependents, DateTime DateOfAdoption, string AddDependent, string DropDependent)
        {
    
            Life_Insurance lifeIns = new Life_Insurance();
   
            lifeIns.Employee_id = Employee_id;
            lifeIns.GroupPlanNumber = GroupPlanNumber;
            lifeIns.BenefitsEffectiveDate = BenefitsEffectiveDate;
            lifeIns.InitialEnrollment = InitialEnrollment;
            lifeIns.ReEnrollment = ReEnrollment;
            lifeIns.AddEmployeeAndDependents = AddEmployeeAndDependents;
            lifeIns.DropRefuseCoverage = DropRefuseCoverage;
            lifeIns.InformationChange = InformationChange;
            lifeIns.IncreaseAmount = IncreaseAmount;
            lifeIns.FamilyStatusChange = FamilyStatusChange;
            lifeIns.SubTotalCode = SubTotalCode;
            lifeIns.MarriedOrHaveSpouse = Married;
            lifeIns.DateOfMarriage = DateOfMarriage;
            lifeIns.HaveChildrenOrHaveDependents = OtherDependents;
            lifeIns.PlacementDateOfAdoptedChild = DateOfAdoption;
            lifeIns.AddDependent = AddDependent;
            lifeIns.DropDependent = DropDependent;
          
            db.Life_Insurance.Add(lifeIns);
            db.SaveChanges();

            int result = lifeIns.LifeInsurance_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LifeInsEnrollmentEmpAndFamilyCoverage(int? LifeInsurance_id, int? Employee_id, string DropEmployee, string DropDependents, 
            DateTime LastDayOfCoverage, string TerminationEmploymentOfDropCoverage, string Retirement, DateTime TerminationEmploymentDateOfDropCoverage, 
            string OtherEvent, string OtherEventReason, DateTime OtherEventDate, string DropBasicLife, string EmployeeDentalDrop, string SpouseDentalDrop, 
            string DependentDentalDrop, string EmployeeVisionDrop, string SpouseVisionDrop, string DependentVisionDrop, string TerminationEmploymentLossOfOtherCoverage,
            DateTime TerminationEmploymentDateLossOfOtherCoverage, string Divorce, DateTime DivorceDate, string DeathOfSpouse, DateTime DeathOfSpouseDate, 
            string TerminationOrExpirationOfCoverage, DateTime TerminationOrExpirationOfCoverageDate, string DentalCoverageLost, string VisionCoverageLost,
            string CoveredUnderOtherInsurance, string Other, string OtherReason, string DentalCoverage, string EmployeeOnlyDental, string EEAndSpouseDental, 
            string EEAndDependentsDental, string EEAndFamilyDental, string DoNotWantDentalCoverage, string EmployeeCoveredUnderOtherDental, string SpouseCoveredUnderOtherDental,
            string DependentsCoveredUnderOtherDental, string VisionCoverage, string EmployeeOnlyVision, string EEAndSpouseVision, string EEAndDependentsVision, 
            string EEAndFamilyVision, string DoNotWantVisionCoverage, string EmployeeCoveredUnderOtherVision, string SpouseCoveredUnderOtherVision,
            string DependentsCoveredUnderOtherVision)
        {
            Life_Insurance lifeIns = db.Life_Insurance
              .Where(i => i.LifeInsurance_id == LifeInsurance_id)
              .Single();

            lifeIns.DropEmployee = DropEmployee;
            lifeIns.DropDependents = DropDependents;
            lifeIns.LastDayOfCoverage = LastDayOfCoverage;
            lifeIns.TerminationEmploymentOfDropCoverage = TerminationEmploymentOfDropCoverage;
            lifeIns.Retirement = Retirement;
            lifeIns.TerminationEmploymentDateOfDropCoverage = TerminationEmploymentDateOfDropCoverage;
            lifeIns.OtherEvent = OtherEvent;
            lifeIns.OtherEventReason = OtherEventReason;
            lifeIns.OtherEventDate = OtherEventDate;
            lifeIns.DropBasicLife = DropBasicLife;
            lifeIns.EmployeeDentalDrop = EmployeeDentalDrop;
            lifeIns.SpouseDentalDrop = SpouseDentalDrop;
            lifeIns.DependentDentalDrop = DependentDentalDrop;
            lifeIns.EmployeeVisionDrop = EmployeeVisionDrop;
            lifeIns.SpouseVisionDrop = SpouseVisionDrop;
            lifeIns.DependentVisionDrop = DependentVisionDrop;
            lifeIns.TerminationEmploymentLossOfOtherCoverage = TerminationEmploymentLossOfOtherCoverage;
            lifeIns.TerminationEmploymentDateLossOfOtherCoverage = TerminationEmploymentDateLossOfOtherCoverage;
            lifeIns.Divorce = Divorce;
            lifeIns.DivorceDate = DivorceDate;
            lifeIns.DeathOfSpouse = DeathOfSpouse;
            lifeIns.DeathOfSpouseDate = DeathOfSpouseDate;
            lifeIns.TerminationOrExpirationOfCoverage = TerminationOrExpirationOfCoverage;
            lifeIns.TerminationOrExpirationOfCoverageDate = TerminationOrExpirationOfCoverageDate;
            lifeIns.DentalCoverageLost = DentalCoverageLost;
            lifeIns.VisionCoverageLost = VisionCoverageLost;
            lifeIns.CoveredUnderOtherInsurance = CoveredUnderOtherInsurance;
            lifeIns.Other = Other;
            lifeIns.OtherReason = OtherReason;
            lifeIns.DentalCoverage = DentalCoverage;
            //lifeIns.EmployeeOnlyDental = EmployeeOnlyDental;
            //lifeIns.EEAndSpouseDental = EEAndSpouseDental;
            //lifeIns.EEAndDependentsDental = EEAndDependentsDental;
            //lifeIns.EEAndFamilyDental = EEAndFamilyDental;
            lifeIns.DoNotWantDentalCoverage = DoNotWantDentalCoverage;
            lifeIns.EmployeeCoveredUnderOtherDentalPlan = EmployeeCoveredUnderOtherDental;
            lifeIns.SpouseCoveredUnderOtherDentalPlan = SpouseCoveredUnderOtherDental;
            lifeIns.DependentsCoveredUnderOtherDentalPlan = DependentsCoveredUnderOtherDental;
            lifeIns.VisionCoverage = VisionCoverage;
            //lifeIns.EmployeeOnlyVision = EmployeeOnlyVision;
            //lifeIns.EEAndSpouseVision = EEAndSpouseVision;
            //lifeIns.EEAndDependentsVision = EEAndDependentsVision;
            //lifeIns.EEAndFamilyVision = EEAndFamilyVision;
            lifeIns.DoNotWantVisionCoverage = DoNotWantVisionCoverage;
            lifeIns.EmployeeCoveredUnderOtherVisionPlan = EmployeeCoveredUnderOtherVision;
            lifeIns.SpouseCoveredUnderOtherVisionPlan = SpouseCoveredUnderOtherVision;
            lifeIns.DependentsCoveredUnderOtherVisionPlan = DependentsCoveredUnderOtherVision;

            db.SaveChanges();

            int result = lifeIns.LifeInsurance_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-LifeIns
        public ActionResult EditLifeInsurance(int? LifeInsurance_id, int? Employee_id)
        {
            EmployeeAndInsuranceVM empAndInsVM = new EmployeeAndInsuranceVM();

            empAndInsVM.lifeIns = db.Life_Insurance.FirstOrDefault(i => i.Employee_id == Employee_id);

            if (LifeInsurance_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Life_Insurance lifeIns = db.Life_Insurance.Find(LifeInsurance_id);
            if (LifeInsurance_id == null)
            {
                return HttpNotFound();
            }

            return View(lifeIns);
        }

        //EditUpdate-LifeIns
        public JsonResult EditLifeInsUpdate(int LifeInsurance_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                         {
                             e.LifeInsurance_id,
                             e.Employee_id,

                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLifeInsurance(int? LifeInsurance_id)
        {
            if (LifeInsurance_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Life_Insurance lifeIns = db.Life_Insurance.Find(LifeInsurance_id);
            if (lifeIns == null)
            {
                return HttpNotFound();
            }

            return View(lifeIns);
        }

        //Delete-LifeInsurance
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteLifeInsurance")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLifeIns(int LifeInsurance_id)
        {
            Life_Insurance lifeIns = db.Life_Insurance.Find(LifeInsurance_id);
            db.Life_Insurance.Remove(lifeIns);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(LifeInsurance_id);

            return RedirectToAction("LifeInsuranceEnrollment");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //LifeInsurance-End-----------------------------------------------------------------------------

    }
}
