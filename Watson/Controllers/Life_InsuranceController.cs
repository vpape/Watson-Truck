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

        public ActionResult LifeInsuranceEnrollment(int? LifeInsurance_id, int? Employee_id, int? Beneficiary_id)
        {
            ViewBag.LifeInsurance_id = LifeInsurance_id;
            ViewBag.Employee_id = Employee_id;
            ViewBag.Beneficiary_id = Beneficiary_id;

            EmployeeAndInsuranceVM empAndInsVM = new EmployeeAndInsuranceVM();

            empAndInsVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            empAndInsVM.lifeIns = db.Life_Insurance.FirstOrDefault(i => i.Employee_id == Employee_id);
            empAndInsVM.benefiList = db.Beneficiaries.Where(i => i.Employee_id == Employee_id).ToList();

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
        public JsonResult LifeInsEnrollmentNew(int LifeInsurance_id, int Employee_id, int InsurancePlan_id, string GroupPlanNumber, DateTime BenefitsEffectiveDate,
            string InitialEnrollment, string ReEnrollment, string AddEmployeeAndDependents, string DropRefuseCoverage, string InformationChange, string IncreaseAmount,
            string FamilyStatusChange, string SubTotalCode, string Married, DateTime DateOfMarriage, string OtherDependents, DateTime? DateOfAdoption, 
            string AddDependent, string DropDependent, string DropEmployee, string DropDependents, DateTime? LastDayOfCoverage, string TerminationEmploymentOfDropCoverage,
            string Retirement, DateTime? LastDayWorked, string OtherEvent, string OtherEventReason, DateTime? OtherEventDate, string DropBasicLife, 
            string EmployeeDentalDrop, string SpouseDentalDrop, string DependentDentalDrop, string EmployeeVisionDrop, string SpouseVisionDrop, string DependentVisionDrop, 
            string TerminationEmploymentLossOfOtherCoverage, DateTime? TerminationEmploymentDateLossOfOtherCoverage, string Divorce, DateTime? DivorceDate, 
            string DeathOfSpouse, DateTime? DeathOfSpouseDate, string TerminationOrExpirationOfCoverage, /*DateTime? TerminationOrExpirationOfCoverageDate,*/
            string DentalCoverageLost, string VisionCoverageLost, string CoveredUnderOtherInsurance, string Other, string OtherReason, string EmployeeOnlyDental,
            string EEAndSpouseDental, string EEAndDependentsDental, string EEAndFamilyDental, string DoNotWantDentalCoverage, string EmployeeCoveredUnderOtherDental,
            string SpouseCoveredUnderOtherDental, string DependentsCoveredUnderOtherDental, string EmployeeOnlyVision, string EEAndSpouseVision, 
            string EEAndDependentsVision, string EEAndFamilyVision, string DoNotWantVisionCoverage, string EmployeeCoveredUnderOtherVision, 
            string SpouseCoveredUnderOtherVision, string DependentsCoveredUnderOtherVision, string PrimaryBeneficiary, string ContingentBeneficiary, 
            string OwnerBasicLifeADandDPolicyAmount, string ManagerBasicLifeADandDPolicyAmount, string EmployeeBasicLifeADandDPolicyAmount,
            string DoNotWantBasicLifeCoverageAandD, string PreviousPolicyAmount, string DentalPlan, string VisionPlan)
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
            lifeIns.DropEmployee = DropEmployee;
            lifeIns.DropDependents = DropDependents;
            lifeIns.LastDayOfCoverage = LastDayOfCoverage;
            lifeIns.TerminationEmploymentOfDropCoverage = TerminationEmploymentOfDropCoverage;
            lifeIns.Retirement = Retirement;
            lifeIns.LastDayWorked = LastDayWorked;
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
            //lifeIns.TerminationOrExpirationOfCoverageDate = TerminationOrExpirationOfCoverageDate;-- NonNullable error Parameter for DateTime
            lifeIns.DentalCoverageLost = DentalCoverageLost;
            lifeIns.VisionCoverageLost = VisionCoverageLost;
            lifeIns.CoveredUnderOtherInsurance = CoveredUnderOtherInsurance;
            lifeIns.Other = Other;
            lifeIns.OtherReason = OtherReason;

            lifeIns.DentalCoverage = DentalPlan;
            lifeIns.VisionCoverage = VisionPlan;

            lifeIns.EmployeeOnlyDental = EmployeeOnlyDental;
            lifeIns.EEAndSpouseDental = EEAndSpouseDental;
            lifeIns.EEAndDependentsDental = EEAndDependentsDental;
            lifeIns.EEAndFamilyDental = EEAndFamilyDental;
            lifeIns.DoNotWantDentalCoverage = DoNotWantDentalCoverage;
            lifeIns.EmployeeCoveredUnderOtherDentalPlan = EmployeeCoveredUnderOtherDental;
            lifeIns.SpouseCoveredUnderOtherDentalPlan = SpouseCoveredUnderOtherDental;
            lifeIns.DependentsCoveredUnderOtherDentalPlan = DependentsCoveredUnderOtherDental;

            lifeIns.EmployeeOnlyVision = EmployeeOnlyVision;
            lifeIns.EEAndSpouseVision = EEAndSpouseVision;
            lifeIns.EEAndDependentsVision = EEAndDependentsVision;
            lifeIns.EEAndFamilyVision = EEAndFamilyVision;
            lifeIns.DoNotWantVisionCoverage = DoNotWantVisionCoverage;
            lifeIns.EmployeeCoveredUnderOtherVisionPlan = EmployeeCoveredUnderOtherVision;
            lifeIns.SpouseCoveredUnderOtherVisionPlan = SpouseCoveredUnderOtherVision;
            lifeIns.DependentsCoveredUnderOtherVisionPlan = DependentsCoveredUnderOtherVision;

            lifeIns.OwnerBasicLifeWithADandDPolicyAmount = OwnerBasicLifeADandDPolicyAmount;
            lifeIns.ManagerBasicLifeWithADandDPolicyAmount = ManagerBasicLifeADandDPolicyAmount;
            lifeIns.EmployeeBasicLifeWithADandDPolicyAmount = EmployeeBasicLifeADandDPolicyAmount;
            lifeIns.DoNotWantBasicLifeCoverageWithADandD = DoNotWantBasicLifeCoverageAandD;
            lifeIns.AmountOfPreviousPolicy = PreviousPolicyAmount;

            db.Life_Insurance.Add(lifeIns);
            db.SaveChanges();

            //InsurancePlan insPlan = db.InsurancePlans
            //.Where(i => i.InsurancePlan_id == InsurancePlan_id)
            //.SingleOrDefault();

            //insPlan.Employee_id = Employee_id;
            //insPlan.DentalPlan = DentalPlan;
            //insPlan.VisionPlan = VisionPlan;

            db.SaveChanges();

            int result = lifeIns.LifeInsurance_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-LifeIns
        public ActionResult EditLifeInsurance(int? LifeInsurance_id, int? Employee_id, int? Beneficiary_id, int? InsurancePlan_id)
        {
            ViewBag.LifeInsurance_id = LifeInsurance_id;
            ViewBag.Employee_id = Employee_id;
            ViewBag.Beneficiary_id = Beneficiary_id;
            ViewBag.InsurancePlan_id = InsurancePlan_id;

            EmployeeAndInsuranceVM empAndInsVM = new EmployeeAndInsuranceVM();

            empAndInsVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            empAndInsVM.lifeIns = db.Life_Insurance.FirstOrDefault(i => i.Employee_id == Employee_id);
            empAndInsVM.benefiList = db.Beneficiaries.Where(i => i.Employee_id == Employee_id).ToList();

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

        //EditUpdate-LifeIns
        public JsonResult EditLifeInsUpdate(int? LifeInsurance_id, int? Employee_id, int? InsurancePlan_id, string GroupPlanNumber, DateTime BenefitsEffectiveDate,
            string InitialEnrollment, string ReEnrollment, string AddEmployeeAndDependents, string DropRefuseCoverage, string InformationChange, string IncreaseAmount,
            string FamilyStatusChange, string SubTotalCode, string Married, DateTime DateOfMarriage, string OtherDependents, DateTime? DateOfAdoption, string AddDependent,
            string DropDependent, string DropEmployee, string DropDependents, DateTime? LastDayOfCoverage, string TerminationEmploymentOfDropCoverage, string Retirement,
            DateTime? LastDayWorked, string OtherEvent, string OtherEventReason, DateTime? OtherEventDate, string DropBasicLife, string EmployeeDentalDrop,
            string SpouseDentalDrop, string DependentDentalDrop, string EmployeeVisionDrop, string SpouseVisionDrop, string DependentVisionDrop,
            string TerminationEmploymentLossOfOtherCoverage, DateTime? TerminationEmploymentDateLossOfOtherCoverage, string Divorce, DateTime? DivorceDate,
            string DeathOfSpouse, DateTime? DeathOfSpouseDate, string TerminationOrExpirationOfCoverage, /*DateTime? TerminationOrExpirationOfCoverageDate,*/
            string DentalCoverageLost, string VisionCoverageLost, string CoveredUnderOtherInsurance, string Other, string OtherReason, string DentalCoverage,
            string EmployeeOnlyDental, string EEAndSpouseDental, string EEAndDependentsDental, string EEAndFamilyDental, string DoNotWantDentalCoverage,
            string EmployeeCoveredUnderOtherDental, string SpouseCoveredUnderOtherDental, string DependentsCoveredUnderOtherDental, string VisionCoverage,
            string EmployeeOnlyVision, string EEAndSpouseVision, string EEAndDependentsVision, string EEAndFamilyVision, string DoNotWantVisionCoverage,
            string EmployeeCoveredUnderOtherVision, string SpouseCoveredUnderOtherVision, string DependentsCoveredUnderOtherVision, string PrimaryBeneficiary,
            string ContingentBeneficiary, string OwnerBasicLifeADandDPolicyAmount, string ManagerBasicLifeADandDPolicyAmount, string EmployeeBasicLifeADandDPolicyAmount,
            string DoNotWantBasicLifeCoverageAandD, string PreviousPolicyAmount, string DentalPlan, string VisionPlan)
        {
            Life_Insurance lifeins = db.Life_Insurance
                .Where(i => i.Employee_id == Employee_id)
                .SingleOrDefault();

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
            lifeIns.DropEmployee = DropEmployee;
            lifeIns.DropDependents = DropDependents;
            lifeIns.LastDayOfCoverage = LastDayOfCoverage;
            lifeIns.TerminationEmploymentOfDropCoverage = TerminationEmploymentOfDropCoverage;
            lifeIns.Retirement = Retirement;
            lifeIns.LastDayWorked = LastDayWorked;
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
            //lifeIns.TerminationOrExpirationOfCoverageDate = TerminationOrExpirationOfCoverageDate;-- NonNullable error Parameter for DateTime
            lifeIns.DentalCoverageLost = DentalCoverageLost;
            lifeIns.VisionCoverageLost = VisionCoverageLost;
            lifeIns.CoveredUnderOtherInsurance = CoveredUnderOtherInsurance;
            lifeIns.Other = Other;
            lifeIns.OtherReason = OtherReason;

            lifeIns.DentalCoverage = DentalPlan;
            lifeIns.VisionCoverage = VisionPlan;

            lifeIns.EmployeeOnlyDental = EmployeeOnlyDental;
            lifeIns.EEAndSpouseDental = EEAndSpouseDental;
            lifeIns.EEAndDependentsDental = EEAndDependentsDental;
            lifeIns.EEAndFamilyDental = EEAndFamilyDental;
            lifeIns.DoNotWantDentalCoverage = DoNotWantDentalCoverage;
            lifeIns.EmployeeCoveredUnderOtherDentalPlan = EmployeeCoveredUnderOtherDental;
            lifeIns.SpouseCoveredUnderOtherDentalPlan = SpouseCoveredUnderOtherDental;
            lifeIns.DependentsCoveredUnderOtherDentalPlan = DependentsCoveredUnderOtherDental;

            lifeIns.EmployeeOnlyVision = EmployeeOnlyVision;
            lifeIns.EEAndSpouseVision = EEAndSpouseVision;
            lifeIns.EEAndDependentsVision = EEAndDependentsVision;
            lifeIns.EEAndFamilyVision = EEAndFamilyVision;
            lifeIns.DoNotWantVisionCoverage = DoNotWantVisionCoverage;
            lifeIns.EmployeeCoveredUnderOtherVisionPlan = EmployeeCoveredUnderOtherVision;
            lifeIns.SpouseCoveredUnderOtherVisionPlan = SpouseCoveredUnderOtherVision;
            lifeIns.DependentsCoveredUnderOtherVisionPlan = DependentsCoveredUnderOtherVision;

            lifeIns.OwnerBasicLifeWithADandDPolicyAmount = OwnerBasicLifeADandDPolicyAmount;
            lifeIns.ManagerBasicLifeWithADandDPolicyAmount = ManagerBasicLifeADandDPolicyAmount;
            lifeIns.EmployeeBasicLifeWithADandDPolicyAmount = EmployeeBasicLifeADandDPolicyAmount;
            lifeIns.DoNotWantBasicLifeCoverageWithADandD = DoNotWantBasicLifeCoverageAandD;
            lifeIns.AmountOfPreviousPolicy = PreviousPolicyAmount;

            //InsurancePlan insPlan = db.InsurancePlans
            //    .Where(i => i.Employee_id == Employee_id)
            //    .SingleOrDefault();

            //ViewBag.InsurancePlan_id = insPlan.InsurancePlan_id;

            //insPlan.DentalPlan = DentalPlan;
            //insPlan.VisionPlan = VisionPlan;

            if (ModelState.IsValid)
            {
                db.Entry(lifeIns).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }

            int result = lifeins.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult AddBeneficiary(int Employee_id, int? Beneficiary_id)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.Beneficiary_id = Beneficiary_id;

            EmployeeAndInsuranceVM empAndInsVM = new EmployeeAndInsuranceVM();

            empAndInsVM.benefiList = db.Beneficiaries.Where(i => i.Beneficiary_id == Beneficiary_id).ToList();

            return View(empAndInsVM);
        }

        public JsonResult AddBeneficiaryUpdate(int Employee_id, int? Beneficiary_id, string PrimaryBeneficiary, string ContingentBeneficiary, string FirstName,
            string LastName, string SSN, string RelationshipToEmployee, DateTime DateOfBirth, string PhoneNumber, string PercentageOfBenefits, string MailingAddress,
            string City, string State, string ZipCode)
        {

            Beneficiary benefi = new Beneficiary();

            benefi.Employee_id = Employee_id;
            benefi.PrimaryBeneficiary = PrimaryBeneficiary;
            benefi.ContingentBeneficiary = ContingentBeneficiary;
            benefi.FirstName = FirstName;
            benefi.LastName = LastName;
            benefi.SSN = SSN;
            benefi.RelationshipToEmployee = RelationshipToEmployee;
            benefi.DOB = DateOfBirth;
            benefi.PhoneNumber = PhoneNumber;
            benefi.PercentageOfBenefits = PercentageOfBenefits;
            benefi.Address = MailingAddress;
            benefi.CIty = City;
            benefi.State = State;
            benefi.ZipCode = ZipCode;

            int result = benefi.Beneficiary_id;

            db.Beneficiaries.Add(benefi);
            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------
        public ActionResult EditBeneficiary(int? Employee_id, int? Beneficiary_id)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.Beneficiary_id = Beneficiary_id;

            EmployeeAndInsuranceVM empAndInsVM = new EmployeeAndInsuranceVM();

            empAndInsVM.beneficiary = db.Beneficiaries.FirstOrDefault(i => i.Employee_id == Employee_id);
            //empAndInsVM.benefiList = db.Beneficiaries.Where(i => i.Employee_id == Employee_id).ToList();

            return View(empAndInsVM);
        }


        public JsonResult EditBeneficaryUpdate(int? Employee_id, int? Beneficiary_id, string PrimaryBeneficiary, string ContingentBeneficiary, string FirstName,
            string LastName, string SSN, string RelationshipToEmployee, string MailingAddress, string City, string State, string ZipCode, DateTime DOB,
            string PhoneNumber, string PercentageOfBenefits)
        {
            Beneficiary benefi = db.Beneficiaries
                     .Where(i => i.Employee_id == Employee_id)
                     .SingleOrDefault();

            ViewBag.Beneficiary_id = benefi.Beneficiary_id;

            benefi.PrimaryBeneficiary = PrimaryBeneficiary;
            benefi.ContingentBeneficiary = ContingentBeneficiary;
            benefi.FirstName = FirstName;
            benefi.LastName = LastName;
            benefi.SSN = SSN;
            benefi.RelationshipToEmployee = RelationshipToEmployee;
            benefi.Address = MailingAddress;
            benefi.CIty = City;
            benefi.State = State;
            benefi.ZipCode = ZipCode;
            benefi.DOB = DOB;
            benefi.PhoneNumber = PhoneNumber;
            benefi.PercentageOfBenefits = PercentageOfBenefits;

            if (ModelState.IsValid)
            {
                db.Entry(benefi).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }

            int result = benefi.Beneficiary_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------


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

        //Delete-Beneficiaries
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteBeneficiary")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBeneficiary(int Beneficiary_id)
        {
            Beneficiary beni = db.Beneficiaries.Find(Beneficiary_id);
            db.Beneficiaries.Remove(beni);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(Beneficiary_id);

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
