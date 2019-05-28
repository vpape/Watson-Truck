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
    public class Group_HealthController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static List<Group_Health> groupIns = new List<Group_Health>();
        private static List<Employee> employee = new List<Employee>();
        private static List<Family_Info> family = new List<Family_Info>();
        private static List<Other_Insurance> otherins = new List<Other_Insurance>();
        private static List<GrpHealthMasterList> grpHMasterList = new List<GrpHealthMasterList>();

        public Group_HealthController()
        {

        }

        public ActionResult GrpHealthInsPremiums()
        {
            return View();
        }

        public JsonResult GrpHealthInsPremiumUpdate(int Employee_id, int InsurancePlan_id, string EmployeeOnly, 
            string EmployeeAndSpouse, string EmployeeAndDependent, string EmployeeAndFamily, decimal YearlyPremiumCost, 
            string InsMECPlan, string InsStndPlan, string InsBuyUpPlan, string DentalPlan, string VisionPlan)
        {
            InsurancePlan insPlan = new InsurancePlan();

            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            ViewBag.insPlan = insPlan;

            InsurancePremium insPremium = new InsurancePremium();

            insPremium.YearlyPremiumCost = YearlyPremiumCost;
            insPremium.EmployeeOnly = EmployeeOnly;
            insPremium.EmployeeAndSpouse = EmployeeAndSpouse;
            insPremium.EmployeeAndDependent = EmployeeAndDependent;
            insPremium.EmployeeAndFamily = EmployeeAndFamily;
            insPremium.YearlyPremiumCost = YearlyPremiumCost;

            ViewBag.insPremium = insPremium;

            db.InsurancePlans.Add(insPlan);
            db.InsurancePremiums.Add(insPremium);

            db.SaveChanges();

            int result = insPlan.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsSupplement()
        {
            return View();
        }

        public JsonResult GrpHealthInsSupplementUpdate(int InsurancePlan_id, string Item, string Detail)
        {
            InsurancePlanDetail insDetail = new InsurancePlanDetail();

            insDetail.Item = Item;
            insDetail.Detail = Detail;

            int result = insDetail.InsurancePlan_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthEnrollment()
        {
            return View();
        }

        public JsonResult GrpHealthEnrollmentNew(int Employee_id, int FamilyMember_id, string empInsuranceCarrier, 
            string empInsPolicyNumber, string GroupName, string IMSGroupNumber, string PhoneNumber, 
            string ReasonForGrpCoverageRefusal, string OtherCoverage, string OtherReason, string Myself, string Spouse,
            string Dependent, string empOtherInsuranceCoverage, DateTime CafeteriaPlanYear,
            string NoneGroupHealthOption, string EmpOnlyGroupHealthOption, string EmpSpGroupHealthOption, 
            string EmpDepGroupHealthOption, string EmpFamGroupHealthOption, string EmpSignature,
            DateTime EmpSignatureDate, string EmpInitials, string OtherSignature, DateTime OtherSignatureDate,
            string empDepartment, string empEnrollmentType, int empPayroll_id, string empClass, decimal empAnnualSalary,
            DateTime empEffectiveDate, int empHrsWkPerWk, string spOtherInsCoverage, string spInsCarrier, 
            string spInsPolicyNumber, string spInsPhoneNumber, string spInsMailingAddress, string spInsPObox,
            string spInsCity, string spInsState, string spInsZipCode, string depOtherInsCoverage,
            string depInsCarrier, string depInsPolicyNumber, string depInsPhoneNumber)
        {
            Group_Health g = new Group_Health();

            g.InsuranceCarrier = empInsuranceCarrier;
            g.PolicyNumber = empInsPolicyNumber;
            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;
            g.PhoneNumber = PhoneNumber;
            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.OtherCoverage = OtherCoverage;
            g.OtherReason = OtherReason;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            g.OtherInsuranceCoverage = empOtherInsuranceCoverage;
            g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoneGroupHealthOption;
            g.EmployeeOnly = EmpOnlyGroupHealthOption;
            g.EmployeeAndSpouse = EmpSpGroupHealthOption;
            g.EmployeeAndDependent = EmpDepGroupHealthOption;
            g.EmployeeAndFamily = EmpFamGroupHealthOption;
            g.EmployeeSignature = EmpSignature;
            g.EmployeeSignatureDate = EmpSignatureDate;
            g.EmployeeInitials = EmpInitials;
            g.OtherSignature = OtherSignature;
            g.OtherSignatureDate = OtherSignatureDate;

            Employee emp = new Employee();

            emp.Department = empDepartment;
            emp.EnrollmentType = empEnrollmentType;
            emp.Payroll_id = empPayroll_id;
            emp.Class = empClass;
            emp.AnnualSalary = empAnnualSalary;
            emp.EffectiveDate = empEffectiveDate;
            emp.HoursWorkedPerWeek = empHrsWkPerWk;

            Other_Insurance o = new Other_Insurance();

            o.CoveredByOtherInsurance = spOtherInsCoverage;
            o.InsuranceCarrier = spInsCarrier;
            o.PolicyNumber = spInsPolicyNumber;
            o.PhoneNumber = spInsPhoneNumber;
            o.MailingAddress = spInsMailingAddress;
            o.PObox = spInsPObox;
            o.City = spInsCity;
            o.State = spInsState;
            o.ZipCode = spInsZipCode;
            o.CoveredByOtherInsurance = depOtherInsCoverage;
            o.InsuranceCarrier = depInsCarrier;
            o.PolicyNumber = depInsPolicyNumber;
            o.PhoneNumber = depInsPhoneNumber;

            Employee e = db.Employees
             .Where(i => i.Employee_id == Employee_id)
             .Single();

            ViewBag.e = e;

            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            ViewBag.sp = sp;

            Family_Info dep = db.Family_Info
               .Where(i => i.FamilyMember_id == FamilyMember_id)
               .Single();

            ViewBag.dep = dep;

            if (ModelState.IsValid)
            {
                db.Other_Insurance.Add(o);
                db.Group_Health.Add(g);

                db.SaveChanges();
            }

            int result = e.Employee_id;
           

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GrpHealthInsEditUpdate(int GrpHealthIns_id, int Employee_id, int FamilyMember_id, 
            int OtherInsurance_id, string empInsuranceCarrier, string empInsPolicyNumber, string GroupName, 
            string IMSGroupNumber, string PhoneNumber, string ReasonForGrpCoverageRefusal, string OtherCoverage, string OtherReason,
            string Myself, string Spouse, string Dependent, string empOtherInsuranceCoverage, DateTime CafeteriaPlanYear,
            string NoneGroupHealthOption, string empOnlyGroupHealthOption, string empSpGroupHealthOption,
            string empDepGroupHealthOption, string empFamGroupHealthOption, string empSignature, DateTime empSignatureDate,
            string empDepartment, string empEnrollmentType, int empPayroll_id, string empClass, string empJobTitle,
            DateTime empHireDate, decimal empAnnualSalary, DateTime empEffectiveDate, int empHrsWkPerWk, string spOtherInsCoverage, string spInsCarrier, string spInsPolicyNumber,
            string spInsPhoneNumber, string spInsMailingAddress, string spInsPObox, string spInsCity, string spInsState,
            string spInsZipCode, string depOtherInsCoverage, string depInsCarrier, string depInsPolicyNumber,
            string depInsPhoneNumber)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GrpHealthIns_id)
                .Single();

            g.InsuranceCarrier = empInsuranceCarrier;
            g.PolicyNumber = empInsPolicyNumber;
            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;
            g.PhoneNumber = PhoneNumber;
            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.OtherCoverage = OtherCoverage;
            g.OtherReason = OtherReason;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            g.OtherInsuranceCoverage = empOtherInsuranceCoverage;
            g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoneGroupHealthOption;
            g.EmployeeOnly = empOnlyGroupHealthOption;
            g.EmployeeAndSpouse = empSpGroupHealthOption;
            g.EmployeeAndDependent = empDepGroupHealthOption;
            g.EmployeeAndFamily = empFamGroupHealthOption;
            g.EmployeeSignature = empSignature;
            g.EmployeeSignatureDate = empSignatureDate;

            ViewBag.g = g;

            Employee e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.Department = empDepartment;
            e.EnrollmentType = empEnrollmentType;
            e.Payroll_id = empPayroll_id;
            e.Class = empClass;
            e.AnnualSalary = empAnnualSalary;
            e.JobTitle = empJobTitle;
            e.HireDate = empHireDate;
            e.EffectiveDate = empEffectiveDate;
            e.HoursWorkedPerWeek = empHrsWkPerWk;

            ViewBag.e = e;

            Other_Insurance o = db.Other_Insurance
               .Where(i => i.OtherInsurance_id == OtherInsurance_id)
               .Single();

            o.CoveredByOtherInsurance = spOtherInsCoverage;
            o.InsuranceCarrier = spInsCarrier;
            o.PolicyNumber = spInsPolicyNumber;
            o.PhoneNumber = spInsPhoneNumber;
            o.MailingAddress = spInsMailingAddress;
            o.PObox = spInsPObox;
            o.City = spInsCity;
            o.State = spInsState;
            o.ZipCode = spInsZipCode;
            o.CoveredByOtherInsurance = depOtherInsCoverage;
            o.InsuranceCarrier = depInsCarrier;
            o.PolicyNumber = depInsPolicyNumber;
            o.PhoneNumber = depInsPhoneNumber;

            ViewBag.o = o;

            if (ModelState.IsValid)
            {
                db.Entry(g).State = System.Data.Entity.EntityState.Modified;
                db.Entry(o).State = System.Data.Entity.EntityState.Modified;

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

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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

        public JsonResult GetGrpHealthInsDetail(int GrpHealthIns_id, int OtherInsurance_id)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GrpHealthIns_id)
                .Single();

            ViewBag.g = g;

            var o = db.Other_Insurance
            .Where(i => i.OtherInsurance_id == OtherInsurance_id)
            .Single();

            ViewBag.o = o;

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult SalaryRedirectAgreement()
        {
            return View();
        }

        
        public JsonResult SalaryRedirectionUpdate(int Employee_id, string MedicalInsProvider, string EEelectionPreTaxMedIns,
            decimal PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, decimal PremiumPostTaxMedIns, string DentalInsProvider,
            string EEelectionPreTaxDentalIns, decimal PremiumPreTaxDentalIns, string EEelectionPostTaxDentalIns,
            decimal PremiumPostTaxDentalIns, string VisionInsProvider, string EEelectionPreTaxVisionIns, 
            decimal PremiumPreTaxVisionIns, string EEelectionPostTaxVisionIns, decimal PremiumPostTaxVisionIns,
            decimal TotalPreTax, decimal TotalPostTax, string empSignature, DateTime empSignatureDate, string empInitials1)
        {
            Employee e = db.Employees
            .Where(i => i.Employee_id == Employee_id)
            .Single();

            ViewBag.e = e;

            Deduction d = new Deduction();

            d.Provider = MedicalInsProvider;
            d.EEelectionPreTax = EEelectionPreTaxMedIns;
            d.PremiumPreTax = PremiumPreTaxMedIns;
            d.EEelectionPostTax = EEelectionPostTaxMedIns;
            d.PremiumPostTax = PremiumPostTaxMedIns;

            d.Provider = DentalInsProvider;
            d.EEelectionPreTax = EEelectionPreTaxDentalIns;
            d.PremiumPreTax = PremiumPreTaxDentalIns;
            d.EEelectionPostTax = EEelectionPostTaxDentalIns;
            d.PremiumPostTax = PremiumPostTaxDentalIns;

            d.Provider = VisionInsProvider;
            d.EEelectionPreTax = EEelectionPreTaxVisionIns;
            d.PremiumPreTax = PremiumPreTaxVisionIns;
            d.EEelectionPostTax = EEelectionPostTaxVisionIns;
            d.PremiumPostTax = PremiumPostTaxVisionIns;

            d.TotalPreTax = TotalPreTax;
            d.TotalPostTax = TotalPostTax;
            d.EmployeeSignature = empSignature;
            d.EmployeeSignatureDate = empSignatureDate;
            d.EmployeeInitials = empInitials1;
         
            ViewBag.d = d;

            db.Deductions.Add(d);
            db.SaveChanges();

            int result = d.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult AuthorizationForm()
        {
            return View();
        }

        public JsonResult AuthorizationFormUpdate(int Employee_id, string NameOfPerson1, string NameOfPerson1Relationship, string NameOfPerson2,
            string NameOfPerson2Relationship, string EmpSignature, DateTime EmpSignatureDate, string NameOfPerson1Signature, 
            DateTime NameOfPerson1SignatureDate, string NameOfPerson2Signature, DateTime NameOfPerson2SignatureDate)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            ViewBag.e = e;

            Group_Health g = new Group_Health();

            g.NameOfPersonToReleaseInfoTo = NameOfPerson1;
            g.Relationship = NameOfPerson1Relationship;
            g.NameOfPersonToReleaseInfoTo = NameOfPerson2;
            g.Relationship = NameOfPerson2Relationship;
            g.EmployeeSignature = EmpSignature;
            g.EmployeeSignatureDate = EmpSignatureDate;
            g.OtherSignature = NameOfPerson1Signature;
            g.OtherSignatureDate = NameOfPerson1SignatureDate;
            g.OtherSignature = NameOfPerson2Signature;
            g.OtherSignatureDate = NameOfPerson2SignatureDate;

            ViewBag.g = g;

            db.Group_Health.Add(g);
            db.SaveChanges();

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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

            db.DeleteEmployeeAndDependents(id);

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
