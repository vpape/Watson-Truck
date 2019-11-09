using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Dynamic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;
using Watson.ViewModels;

namespace Watson.Controllers
{
    public class Group_HealthController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        private static Group_Health grpHealth = new Group_Health();
        private static Employee employee = new Employee();
        private static List<Family_Info> family = new List<Family_Info>();
        private static List<Other_Insurance> otherIns = new List<Other_Insurance>();

        //use for dependency injection
        private IEmployeeRepository _employeeRepository;

        public Group_HealthController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Group_HealthController()
        {

        }

        //GroupHealth-Start---------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsPremiums()
        {
            return View();
        }

        //Create-InsPrem---Finish Html page for Admin. Employee will only view Ins Cost and Vision and Dental pdf
        public JsonResult GrpHealthInsPremiumNew(int Employee_id, int InsurancePremium_id, string EmployeeOnly, 
            string EmployeeAndSpouse, string EmployeeAndDependent, string EmployeeAndFamily, decimal YearlyPremiumCost)
        {
            InsurancePremium insPremium = new InsurancePremium();

            insPremium.EmployeeOnly = EmployeeOnly;
            insPremium.EmployeeAndSpouse = EmployeeAndSpouse;
            insPremium.EmployeeAndDependent = EmployeeAndDependent;
            insPremium.EmployeeAndFamily = EmployeeAndFamily;
            insPremium.YearlyPremiumCost = YearlyPremiumCost;

            ViewBag.insPremium = insPremium;

            Employee e = db.Employees
            .Where(i => i.Employee_id == Employee_id)
            .Single();

            db.InsurancePremiums.Add(insPremium);

            db.SaveChanges();

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //Edit-InsPrem
        public ActionResult EditGrpHealthInsPremium(int? InsurancePremium_id)
        {
            if (InsurancePremium_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InsurancePremium insPremium = db.InsurancePremiums.Find(InsurancePremium_id);
            if (insPremium == null)
            {
                return HttpNotFound();
            }

            ViewBag.InsurancePremium = insPremium.InsurancePremium_id;

            return View(insPremium);
        }

        //EditUpdate-InsPrem
        public JsonResult GrpHealthInsPremiumEditUpdate(int Employee_id, int InsurancePremium_id, string EmployeeOnly,
            string EmployeeAndSpouse, string EmployeeAndDependent, string EmployeeAndFamily, decimal YearlyPremiumCost)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            InsurancePremium insPremium = db.InsurancePremiums
                .Where(i => i.InsurancePremium_id == InsurancePremium_id)
                .Single();

            insPremium.EmployeeOnly = EmployeeOnly;
            insPremium.EmployeeAndSpouse = EmployeeAndSpouse;
            insPremium.EmployeeAndDependent = EmployeeAndDependent;
            insPremium.EmployeeAndFamily = EmployeeAndFamily;
            insPremium.YearlyPremiumCost = YearlyPremiumCost;

            ViewBag.insPremium = insPremium;

            if (ModelState.IsValid)
            {
                db.Entry(insPremium).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { e.Employee_id });
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthInsSupplement()
        {
            return View();
        }

        //Create-InsSupplment---Finish Html page for Admin. Employee will only view Ins Cost and Vision and Dental pdf
        //ask about the Dental and Vision Cost sheet premimums- do they need to be added to db.InsPremimum table
        public JsonResult GrpHealthInsSupplementNew(int InsurancePlanDetail_id, string CalendarYearDeductible, string WaivedForPreventive, 
            string AnnualMaximum, string Preventive, string Basic, string Major, string UCRpercentage, string EndoPeridontics,
            string Orthodontia, string OrthodontiaLifetimeMax, string WaitingPeriod, string DentalNetWork, string Exams, 
            string Materials, string LensesSingleVision, string BiFocal, string TriFocal, string Lenticular, 
            string ContactsMedicallyNecessary, string ContactsElective, string Frames, string Network, string DentalNetwork,
            string RateGuarantee, string Item, string Detail)
        {
            InsurancePlanDetail insPlanDetail = new InsurancePlanDetail();

            //"EmpDentalCost": EmpDentalCost,
            //"EmpVisionCost": EmpVisionCost,
            //"EmpSpDentalCost": EmpSpDentalCost,
            //"EmpSpVisionCost": EmpSpVisionCost,
            //"EmpDepDentalCost": EmpDepDentalCost,
            //"EmpDepVisionCost": EmpDepVisionCost,
            //"EmpFamDentalCost": EmpFamDentalCost,
            //"EmpFamVisionCost": EmpFamVisionCost,

            insPlanDetail.CalendarYearDeductible = CalendarYearDeductible;
            insPlanDetail.WaivedForPreventive = WaivedForPreventive;
            insPlanDetail.AnnualMaximum = AnnualMaximum;
            insPlanDetail.Preventive = Preventive;
            insPlanDetail.Basic = Basic;
            insPlanDetail.Major = Major;
            insPlanDetail.UCRPercentage = UCRpercentage;
            insPlanDetail.EndodonticsOrPeriodontics = EndoPeridontics;
            insPlanDetail.Orthodontia = Orthodontia;
            insPlanDetail.OrthodontiaLifeTimeMaximum = OrthodontiaLifetimeMax;
            insPlanDetail.WaitingPeriod = WaitingPeriod;
            insPlanDetail.DentalNetwork = DentalNetWork;

            insPlanDetail.Exams = Exams;
            insPlanDetail.Materials = Materials;
            insPlanDetail.LensesSingleVision = LensesSingleVision;
            insPlanDetail.Bifocal = BiFocal;
            insPlanDetail.Trifocal = TriFocal;
            insPlanDetail.Lenticular = Lenticular;
            insPlanDetail.ContactMedicallyNecessary = ContactsMedicallyNecessary;
            insPlanDetail.ContactElective = ContactsElective;
            insPlanDetail.Frames = Frames;
            insPlanDetail.Network = Network;
            insPlanDetail.RateGuarantee = RateGuarantee;

            insPlanDetail.Item = Item;
            insPlanDetail.Detail = Detail;

            int result = insPlanDetail.InsurancePlanDetail_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-InsSupplment
        public ActionResult EditGrpHealthSupplement(int? InsurancePlanDetail_id)
        {
            if (InsurancePlanDetail_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InsurancePlanDetail insPlanDetail = db.InsurancePlanDetails.Find(InsurancePlanDetail_id);
            if (insPlanDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.InsurancePlanDetail = insPlanDetail.InsurancePlan_id;

            return View(insPlanDetail);
        }

        //EditUpdate-InsSupplment
        public JsonResult GrpHealthInsSupplementEditUpdate(int InsurancePlanDetail_id, string CalendarYearDeductible, string WaivedForPreventive,
            string AnnualMaximum, string Preventive, string Basic, string Major, string UCRpercentage, string EndoPeridontics,
            string Orthodontia, string OrthodontiaLifetimeMax, string WaitingPeriod, string DentalNetWork, string Exams,
            string Materials, string LensesSingleVision, string BiFocal, string TriFocal, string Lenticular,
            string ContactsMedicallyNecessary, string ContactsElective, string Frames, string Network, string DentalNetwork,
            string RateGuarantee, string Item, string Detail)
        {
            var insPlanDetail = db.InsurancePlanDetails
                .Where(i => i.InsurancePlanDetail_id == InsurancePlanDetail_id)
                .Single();

            //"EmpDentalCost": EmpDentalCost,
            //"EmpVisionCost": EmpVisionCost,
            //"EmpSpDentalCost": EmpSpDentalCost,
            //"EmpSpVisionCost": EmpSpVisionCost,
            //"EmpDepDentalCost": EmpDepDentalCost,
            //"EmpDepVisionCost": EmpDepVisionCost,
            //"EmpFamDentalCost": EmpFamDentalCost,
            //"EmpFamVisionCost": EmpFamVisionCost,

            insPlanDetail.CalendarYearDeductible = CalendarYearDeductible;
            insPlanDetail.WaivedForPreventive = WaivedForPreventive;
            insPlanDetail.AnnualMaximum = AnnualMaximum;
            insPlanDetail.Preventive = Preventive;
            insPlanDetail.Basic = Basic;
            insPlanDetail.Major = Major;
            insPlanDetail.UCRPercentage = UCRpercentage;
            insPlanDetail.EndodonticsOrPeriodontics = EndoPeridontics;
            insPlanDetail.Orthodontia = Orthodontia;
            insPlanDetail.OrthodontiaLifeTimeMaximum = OrthodontiaLifetimeMax;
            insPlanDetail.WaitingPeriod = WaitingPeriod;
            insPlanDetail.DentalNetwork = DentalNetWork;

            insPlanDetail.Exams = Exams;
            insPlanDetail.Materials = Materials;
            insPlanDetail.LensesSingleVision = LensesSingleVision;
            insPlanDetail.Bifocal = BiFocal;
            insPlanDetail.Trifocal = TriFocal;
            insPlanDetail.Lenticular = Lenticular;
            insPlanDetail.ContactMedicallyNecessary = ContactsMedicallyNecessary;
            insPlanDetail.ContactElective = ContactsElective;
            insPlanDetail.Frames = Frames;
            insPlanDetail.Network = Network;
            insPlanDetail.RateGuarantee = RateGuarantee;

            insPlanDetail.Item = Item;
            insPlanDetail.Detail = Detail;

            ViewBag.insPlanDetail = insPlanDetail;

            if (ModelState.IsValid)
            {
                db.Entry(insPlanDetail).State = System.Data.Entity.EntityState.Modified;
     
                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { insPlanDetail.InsurancePlan_id });
            }

            int result = insPlanDetail.InsurancePlan_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult GrpHealthEnrollment(int? Employee_id, int? GroupHealthInsurance_id)
        {
            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;
            ViewBag.Employee_id = Employee_id;

            GroupHealthGrpHEnrollmentVM groupHGrpHEnrollmentVM = new GroupHealthGrpHEnrollmentVM();

            groupHGrpHEnrollmentVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            groupHGrpHEnrollmentVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);

            groupHGrpHEnrollmentVM.spouse = db.Family_Info.FirstOrDefault(i=> i.Employee_id == Employee_id && i.RelationshipToInsured == "Spouse");
            groupHGrpHEnrollmentVM.family = db.Family_Info.Where(i => i.Employee_id == Employee_id && i.RelationshipToInsured != "Spouse").ToList();
            if (groupHGrpHEnrollmentVM.spouse != null)
            {
                groupHGrpHEnrollmentVM.spouseInsurance = db.Other_Insurance.FirstOrDefault(i => i.Employee_id == Employee_id && i.FamilyMember_id == groupHGrpHEnrollmentVM.spouse.FamilyMember_id);
                groupHGrpHEnrollmentVM.otherIns        = db.Other_Insurance.Where(         i => i.Employee_id == Employee_id && i.FamilyMember_id != groupHGrpHEnrollmentVM.spouse.FamilyMember_id).ToList();
            }
            else
            {
                groupHGrpHEnrollmentVM.spouseInsurance = null;
                groupHGrpHEnrollmentVM.otherIns = db.Other_Insurance.Where(i => i.Employee_id == Employee_id).ToList();
            }

            return View(groupHGrpHEnrollmentVM);

        }

         public JsonResult EmploymentInfoGrpHealthEnrollment(int? Employee_id, string GroupName, string IMSGroupNumber, string Department,
             string EnrollmentType, string Payroll_id, string Class, string AnnualSalary, DateTime EffectiveDate, string HoursWorkedPerWeek)
        {
            //Employee emp = new Employee();
            Employee emp = db.Employees
             .Where(i => i.Employee_id == Employee_id)
             .Single();

            emp.Department = Department;
            emp.EnrollmentType = EnrollmentType;
            emp.Payroll_id = Payroll_id;
            emp.Class = Class;
            emp.AnnualSalary = AnnualSalary;
            emp.EffectiveDate = EffectiveDate;
            emp.HoursWorkedPerWeek = HoursWorkedPerWeek;

            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;

            db.SaveChanges();

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Create-GrpHealthEnrollment- when saving info below, the objects commented out were making it fail
        public JsonResult GrpHealthEnrollmentNew(int? Employee_id, /*int InsurancePlan_id,*/ string OtherCoverageSelection, string OtherReasonSelection,
            string ReasonForGrpCoverageRefusal, string Myself, string Spouse, string Dependent, /*DateTime CafeteriaPlanYear,*/
            string NoneGroupHealthOption, string empOnlyGroupHealthOption, string empSpGroupHealthOption, string empDepGroupHealthOption, 
            string empFamGroupHealthOption, string GrpHEnrollmentEmpSignature, /*DateTime GrpHEnrollmentEmpSignatureDate,*/
            string GrpHRefusalEmpSignature, /*DateTime GrpHRefusalEmpSignatureDate,*/  string InsMECPlan, string InsStndPlan,
            string InsBuyUpPlan, string DentalPlan, string VisionPlan)
        {
           
            Group_Health g = db.Group_Health
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            g.OtherCoverage = OtherCoverageSelection;
            g.OtherReason = OtherReasonSelection;
            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            //g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoneGroupHealthOption;
            g.EmployeeOnly = empOnlyGroupHealthOption;
            g.EmployeeAndSpouse = empSpGroupHealthOption;
            g.EmployeeAndDependent = empDepGroupHealthOption;
            g.EmployeeAndFamily = empFamGroupHealthOption;
            g.GrpHEnrollmentEmpSignature = GrpHEnrollmentEmpSignature;
            //g.GrpHEnrollmentEmpSignatureDate = GrpHEnrollmentEmpSignatureDate;
            g.GrpHRefusalEmpSignature = GrpHRefusalEmpSignature;
            //g.GrpHRefusalEmpSignatureDate = GrpHRefusalEmpSignatureDate;


            InsurancePlan insPlan = new InsurancePlan();

            //insPlan.InsurancePlan_id = InsurancePlan_id;
            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            ViewBag.insPlan = insPlan;


            if (ModelState.IsValid)
            {
                //db.InsurancePlans.Add(insPlan);
                db.SaveChanges();
            }

            int result = g.Employee_id;
           

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-GrpHealthEnrollment
        public ActionResult EditGroupHealthIns(int? Employee_id, int? GroupHealthInsurance_id)
        {
            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;
            ViewBag.Employee_id = Employee_id;

            GroupHealthGrpHEnrollmentVM groupHGrpHEnrollmentVM = new GroupHealthGrpHEnrollmentVM();

            groupHGrpHEnrollmentVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            groupHGrpHEnrollmentVM.family = db.Family_Info.Where(i => i.Employee_id == Employee_id).ToList();
            groupHGrpHEnrollmentVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);
            groupHGrpHEnrollmentVM.otherIns = db.Other_Insurance.Where(i => i.Employee_id == Employee_id).ToList();

            if (GroupHealthInsurance_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(GroupHealthInsurance_id);
            if (g == null)
            {
                return HttpNotFound();
            }

            return View(groupHGrpHEnrollmentVM);
        }

        //EditUpdate-GrpHealthEnrollment
        public JsonResult GrpHealthInsEditUpdate(int GroupHealthInsurance_id, int Employee_id, int InsurancePlan_id, int FamilyMember_id, 
            int OtherInsurance_id, string empInsuranceCarrier, string empInsPolicyNumber, string GroupName, string IMSGroupNumber, 
            string PhoneNumber, string ReasonForGrpCoverageRefusal, string OtherCoverage, string OtherReason,
            string Myself, string Spouse, string Dependent, string empOtherInsuranceCoverage, DateTime CafeteriaPlanYear,
            string NoneGroupHealthOption, string empOnlyGroupHealthOption, string empSpGroupHealthOption, string empDepGroupHealthOption,
            string empFamGroupHealthOption, string GrpHRefusalEmpSignature, DateTime GrpHRefusalEmpSignatureDate, string GrpHEnrollmentEmpSignature,
            DateTime GrpHEnrollmentEmpSignatureDate, string empDepartment, string empEnrollmentType, string empPayroll_id, string empClass,
            string empJobTitle, DateTime empHireDate, string empAnnualSalary, DateTime empEffectiveDate, string empHrsWkPerWk, string InsMECPlan,
            string InsStndPlan, string InsBuyUpPlan, string DentalPlan, string VisionPlan, string spOtherInsCoverage, string spInsCarrier,
            string spInsPolicyNumber, string spInsPhoneNumber, string spInsMailingAddress, string spInsPObox, string spInsCity, string spInsState,
            string spInsZipCode,  string depOtherInsCoverage, string depInsCarrier, string depInsPolicyNumber, string depInsPhoneNumber)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GroupHealthInsurance_id)
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
            g.GrpHRefusalEmpSignature = GrpHRefusalEmpSignature;
            g.GrpHRefusalEmpSignatureDate = GrpHRefusalEmpSignatureDate;
            g.GrpHEnrollmentEmpSignature = GrpHEnrollmentEmpSignature;
            g.GrpHEnrollmentEmpSignatureDate = GrpHEnrollmentEmpSignatureDate;

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

            InsurancePlan insPlan = db.InsurancePlans
              .Where(i => i.InsurancePlan_id == InsurancePlan_id)
              .Single();

            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            ViewBag.insPlan = insPlan;

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

        public ActionResult GrpHealthInsDetail(int? GroupHealthInsurance_id)
        {
            if (GroupHealthInsurance_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group_Health g = db.Group_Health.Find(GroupHealthInsurance_id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        public JsonResult GetGrpHealthInsDetail(int GroupHealthInsurance_id, int OtherInsurance_id)
        {
            var g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GroupHealthInsurance_id)
                .Single();

            ViewBag.g = g;

            var o = db.Other_Insurance
            .Where(i => i.OtherInsurance_id == OtherInsurance_id)
            .Single();

            ViewBag.o = o;

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //GroupHealth-End---------------------------------------------------------------------------------------

        //SalaryRedirect-Start----------------------------------------------------------------------------------

        public ActionResult SalaryRedirection(int Employee_id)
        {

            GroupHealthGrpHEnrollmentVM groupHGrpHEnrollmentVM = new GroupHealthGrpHEnrollmentVM();

            groupHGrpHEnrollmentVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);

            ViewBag.Employee_id = groupHGrpHEnrollmentVM.employee.Employee_id;


            return View(groupHGrpHEnrollmentVM);
        }

        //Create-SalaryRedirect
        public JsonResult SalaryRedirectionUpdate(int Employee_id, int? Deductions_id, string MedicalInsProvider, string EEelectionPreTaxMedIns,
            string PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, string PremiumPostTaxMedIns, string DentalInsProvider, string EEelectionPreTaxDentalIns, 
            string PremiumPreTaxDentalIns, string EEelectionPostTaxDentalIns, string PremiumPostTaxDentalIns, string VisionInsProvider, string EEelectionPreTaxVisionIns, 
            string PremiumPreTaxVisionIns, string EEelectionPostTaxVisionIns, string PremiumPostTaxVisionIns, string AccidentInsProvider, 
            string EEelectionPreTaxAccidentIns, string PremiumPreTaxAccidentIns, string EEelectionPostTaxAccidentIns, string PremiumPostTaxAccidentIns, 
            string CancerInsProvider, string EEelectionPreTaxCancerIns, string PremiumPreTaxCancerIns, string EEelectionPostTaxCancerIns, 
            string PremiumPostTaxCancerIns, string StDisabilityProvider, string EEelectionPreTaxStDisability, string PremiumPreTaxStDisability, 
            string EEelectionPostTaxStDisability, string PremiumPostTaxStDisability, string HospitalIndemProvider, string EEelectionPreTaxHospitalIndem,
            string PremiumPreTaxHospitalIndem, string EEelectionPostTaxHospitalIndem, string PremiumPostTaxHospitalIndem, string TermLifeInsProvider,
            string EEelectionPreTaxTermLifeIns, string PremiumPreTaxTermLifeIns, string EEelectionPostTaxTermLifeIns, string PremiumPostTaxTermLifeIns,
            string WholeLifeInsProvider, string EEelectionPreTaxWholeLifeIns, string PremiumPreTaxWholeLifeIns, string EEelectionPostTaxWholeLifeIns, 
            string PremiumPostTaxWholeLifeIns, string OtherInsProvider, string EEelectionPreTaxOtherIns, string PremiumPreTaxOtherIns, string EEelectionPostTaxOtherIns,
            string PremiumPostTaxOtherIns, string TotalPreTax, string TotalPostTax, string empInitials1, string PreTaxBenefitWaiverinitials, string empSignature,
            DateTime empSignatureDate)
        {
    
            Deduction d = new Deduction();

            d.Employee_id = Employee_id;
            d.MedicalInsProvider = MedicalInsProvider;
            d.EEelectionPreTaxMedIns = EEelectionPreTaxMedIns;
            d.PremiumPreTaxMedIns = PremiumPreTaxMedIns;
            d.EEelectionPostTaxMedIns = EEelectionPostTaxMedIns;
            d.PremiumPostTaxMedIns = PremiumPostTaxMedIns;

            d.DentalInsProvider = DentalInsProvider;
            d.EEelectionPreTaxDentalIns = EEelectionPreTaxDentalIns;
            d.PremiumPreTaxDentalIns = PremiumPreTaxDentalIns;
            d.EEelectionPostTaxDentalIns = EEelectionPostTaxDentalIns;
            d.PremiumPostTaxDentalIns = PremiumPostTaxDentalIns;

            d.VisionInsProvider = VisionInsProvider;
            d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            d.AccidentInsProvider = AccidentInsProvider;
            d.EEelectionPreTaxAccidentIns = EEelectionPreTaxAccidentIns;
            d.PremiumPreTaxAccidentIns = PremiumPreTaxAccidentIns;
            d.EEelectionPostTaxAccidentIns = EEelectionPostTaxAccidentIns;
            d.PremiumPostTaxAccidentIns = PremiumPostTaxAccidentIns;

            d.CancerInsProvider = CancerInsProvider;
            d.EEelectionPreTaxCancerIns = EEelectionPreTaxCancerIns;
            d.PremiumPreTaxCancerIns = PremiumPreTaxCancerIns;
            d.EEelectionPostTaxCancerIns = EEelectionPostTaxCancerIns;
            d.PremiumPostTaxCancerIns = PremiumPostTaxCancerIns;

            d.StDisabilityProvider = StDisabilityProvider;
            d.EEelectionPreTaxStDisability = EEelectionPreTaxStDisability;
            d.PremiumPreTaxStDisability = PremiumPreTaxStDisability;
            d.EEelectionPostTaxStDisability = EEelectionPostTaxStDisability;
            d.PremiumPostTaxStDisability = PremiumPostTaxStDisability;

            d.HospitalIndemProvider = HospitalIndemProvider;
            d.EEelectionPreTaxHospitalIndem = EEelectionPreTaxHospitalIndem;
            d.PremiumPreTaxHospitalIndem = PremiumPreTaxHospitalIndem;
            d.EEelectionPostTaxHospitalIndem = EEelectionPostTaxHospitalIndem;
            d.PremiumPostTaxHospitalIndem = PremiumPostTaxHospitalIndem;

            d.TermLifeInsProvider = TermLifeInsProvider;
            d.EEelectionPreTaxTermLifeIns = EEelectionPreTaxTermLifeIns;
            d.PremiumPreTaxTermLifeIns = PremiumPreTaxTermLifeIns;
            d.EEelectionPostTaxTermLifeIns = EEelectionPostTaxTermLifeIns;
            d.PremiumPostTaxTermLifeIns = PremiumPostTaxTermLifeIns;

            d.WholeLifeInsProvider = WholeLifeInsProvider;
            d.EEelectionPreTaxWholeLifeIns = EEelectionPreTaxWholeLifeIns;
            d.PremiumPreTaxWholeLifeIns = PremiumPreTaxWholeLifeIns;
            d.EEelectionPostTaxWholeLifeIns = EEelectionPostTaxWholeLifeIns;
            d.PremiumPostTaxWholeLifeIns = PremiumPostTaxWholeLifeIns;

            d.OtherInsProvider = OtherInsProvider;
            d.EEelectionPreTaxOtherIns = EEelectionPreTaxOtherIns;
            d.PremiumPreTaxOtherIns = PremiumPreTaxOtherIns;
            d.EEelectionPostTaxOtherIns = EEelectionPostTaxOtherIns;
            d.PremiumPostTaxOtherIns = PremiumPostTaxOtherIns;

            d.TotalPreTax = TotalPreTax;
            d.TotalPostTax = TotalPostTax;
            d.EmployeeSignature = empSignature;
            d.EmployeeSignatureDate = empSignatureDate;
            d.EmployeeInitials = empInitials1;
            d.PreTaxBenefitWaiverinitials = PreTaxBenefitWaiverinitials;


            db.Deductions.Add(d);
            db.SaveChanges();

            int result = d.Deductions_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EditSalaryRedirection(int? Employee_id, int? Deductions_id)
        {
            GroupHealthGrpHEnrollmentVM grpHGrpEnrollmentVM = new GroupHealthGrpHEnrollmentVM();

            grpHGrpEnrollmentVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            grpHGrpEnrollmentVM.deduction = db.Deductions.FirstOrDefault(i => i.Employee_id == Employee_id);

            ViewBag.Deductions_id = grpHGrpEnrollmentVM.deduction.Deductions_id;
            ViewBag.Employee_id = grpHGrpEnrollmentVM.employee.Employee_id;

            //    if (Employee_id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }

            //    Group_Health g = db.Group_Health.Find(Employee_id);
            //    if (g == null)
            //    {
            //        return HttpNotFound();
            //    }

            return View(grpHGrpEnrollmentVM);
        }

        //EditUpdate-SalaryRedirect
        public JsonResult SalaryRedirectionEditUpdate(int Employee_id, int? Deductions_id, string MedicalInsProvider, string EEelectionPreTaxMedIns,
            string PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, string PremiumPostTaxMedIns, string DentalInsProvider, string EEelectionPreTaxDentalIns,
            string PremiumPreTaxDentalIns, string EEelectionPostTaxDentalIns, string PremiumPostTaxDentalIns, string VisionInsProvider, string EEelectionPreTaxVisionIns,
            string PremiumPreTaxVisionIns, string EEelectionPostTaxVisionIns, string PremiumPostTaxVisionIns, string AccidentInsProvider,
            string EEelectionPreTaxAccidentIns, string PremiumPreTaxAccidentIns, string EEelectionPostTaxAccidentIns, string PremiumPostTaxAccidentIns,
            string CancerInsProvider, string EEelectionPreTaxCancerIns, string PremiumPreTaxCancerIns, string EEelectionPostTaxCancerIns,
            string PremiumPostTaxCancerIns, string StDisabilityProvider, string EEelectionPreTaxStDisability, string PremiumPreTaxStDisability,
            string EEelectionPostTaxStDisability, string PremiumPostTaxStDisability, string HospitalIndemProvider, string EEelectionPreTaxHospitalIndem,
            string PremiumPreTaxHospitalIndem, string EEelectionPostTaxHospitalIndem, string PremiumPostTaxHospitalIndem, string TermLifeInsProvider,
            string EEelectionPreTaxTermLifeIns, string PremiumPreTaxTermLifeIns, string EEelectionPostTaxTermLifeIns, string PremiumPostTaxTermLifeIns,
            string WholeLifeInsProvider, string EEelectionPreTaxWholeLifeIns, string PremiumPreTaxWholeLifeIns, string EEelectionPostTaxWholeLifeIns,
            string PremiumPostTaxWholeLifeIns, string OtherInsProvider, string EEelectionPreTaxOtherIns, string PremiumPreTaxOtherIns, string EEelectionPostTaxOtherIns, 
            string PremiumPostTaxOtherIns, string TotalPreTax, string TotalPostTax, string empInitials1, string PreTaxBenefitWaiverinitials, string empSignature, 
            DateTime empSignatureDate)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            ViewBag.e = e;

            Deduction d = db.Deductions
                .Where(i => i.Deductions_id == Deductions_id)
                .Single();

            d.Employee_id = Employee_id;
            d.MedicalInsProvider = MedicalInsProvider;
            d.EEelectionPreTaxMedIns = EEelectionPreTaxMedIns;
            d.PremiumPreTaxMedIns = PremiumPreTaxMedIns;
            d.EEelectionPostTaxMedIns = EEelectionPostTaxMedIns;
            d.PremiumPostTaxMedIns = PremiumPostTaxMedIns;

            d.DentalInsProvider = DentalInsProvider;
            d.EEelectionPreTaxDentalIns = EEelectionPreTaxDentalIns;
            d.PremiumPreTaxDentalIns = PremiumPreTaxDentalIns;
            d.EEelectionPostTaxDentalIns = EEelectionPostTaxDentalIns;
            d.PremiumPostTaxDentalIns = PremiumPostTaxDentalIns;

            d.VisionInsProvider = VisionInsProvider;
            d.EEelectionPreTaxVisionIns = EEelectionPreTaxVisionIns;
            d.PremiumPreTaxVisionIns = PremiumPreTaxVisionIns;
            d.EEelectionPostTaxVisionIns = EEelectionPostTaxVisionIns;
            d.PremiumPostTaxVisionIns = PremiumPostTaxVisionIns;

            d.AccidentInsProvider = AccidentInsProvider;
            d.EEelectionPreTaxAccidentIns = EEelectionPreTaxAccidentIns;
            d.PremiumPreTaxAccidentIns = PremiumPreTaxAccidentIns;
            d.EEelectionPostTaxAccidentIns = EEelectionPostTaxAccidentIns;
            d.PremiumPostTaxAccidentIns = PremiumPostTaxAccidentIns;

            d.CancerInsProvider = CancerInsProvider;
            d.EEelectionPreTaxCancerIns = EEelectionPreTaxCancerIns;
            d.PremiumPreTaxCancerIns = PremiumPreTaxCancerIns;
            d.EEelectionPostTaxCancerIns = EEelectionPostTaxCancerIns;
            d.PremiumPostTaxCancerIns = PremiumPostTaxCancerIns;

            d.StDisabilityProvider = StDisabilityProvider;
            d.EEelectionPreTaxStDisability = EEelectionPreTaxStDisability;
            d.PremiumPreTaxStDisability = PremiumPreTaxStDisability;
            d.EEelectionPostTaxStDisability = EEelectionPostTaxStDisability;
            d.PremiumPostTaxStDisability = PremiumPostTaxStDisability;

            d.HospitalIndemProvider = HospitalIndemProvider;
            d.EEelectionPreTaxHospitalIndem = EEelectionPreTaxHospitalIndem;
            d.PremiumPreTaxHospitalIndem = PremiumPreTaxHospitalIndem;
            d.EEelectionPostTaxHospitalIndem = EEelectionPostTaxHospitalIndem;
            d.PremiumPostTaxHospitalIndem = PremiumPostTaxHospitalIndem;

            d.TermLifeInsProvider = TermLifeInsProvider;
            d.EEelectionPreTaxTermLifeIns = EEelectionPreTaxTermLifeIns;
            d.PremiumPreTaxTermLifeIns = PremiumPreTaxTermLifeIns;
            d.EEelectionPostTaxTermLifeIns = EEelectionPostTaxTermLifeIns;
            d.PremiumPostTaxTermLifeIns = PremiumPostTaxTermLifeIns;

            d.WholeLifeInsProvider = WholeLifeInsProvider;
            d.EEelectionPreTaxWholeLifeIns = EEelectionPreTaxWholeLifeIns;
            d.PremiumPreTaxWholeLifeIns = PremiumPreTaxWholeLifeIns;
            d.EEelectionPostTaxWholeLifeIns = EEelectionPostTaxWholeLifeIns;
            d.PremiumPostTaxWholeLifeIns = PremiumPostTaxWholeLifeIns;

            d.OtherInsProvider = OtherInsProvider;
            d.EEelectionPreTaxOtherIns = EEelectionPreTaxOtherIns;
            d.PremiumPreTaxOtherIns = PremiumPreTaxOtherIns;
            d.EEelectionPostTaxOtherIns = EEelectionPostTaxOtherIns;
            d.PremiumPostTaxOtherIns = PremiumPostTaxOtherIns;

            d.TotalPreTax = TotalPreTax;
            d.TotalPostTax = TotalPostTax;
            d.EmployeeSignature = empSignature;
            d.EmployeeSignatureDate = empSignatureDate;
            d.EmployeeInitials = empInitials1;
            d.PreTaxBenefitWaiverinitials = PreTaxBenefitWaiverinitials;

            if (ModelState.IsValid)
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("EmpOverview", new { d.Employee_id });
            }

            int result = d.Deductions_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //AuthorizationForm-Start-----------------------------------------------------------------------------

        public ActionResult AuthorizationForm(int? Employee_id, int? GroupHealthInsurance_id)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;

            EmployeeAndInsuranceVM employeeAndInsVM = new EmployeeAndInsuranceVM();

            employeeAndInsVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            employeeAndInsVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);


            return View(employeeAndInsVM);
        }

        //Create-AuthorizationForm
        public JsonResult AuthorizationFormNew(int? GroupHealthInsurance_id, int Employee_id, string PersonOneReleaseInfoTo, string PersonOneRelationship,
            string PersonTwoReleaseInfoTo, string PersonTwoRelationship, string PolicyHolderSignature, DateTime? PolicyHolderSignatureDate,
            string PersonOneSignature, DateTime? PersonOneSignatureDate, string PersonTwoSignature, DateTime? PersonTwoSignatureDate)
        {

            Group_Health g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GroupHealthInsurance_id)
                .SingleOrDefault();

            g.Employee_id = Employee_id;
            g.NameOfPersonOneReleaseInfoTo = PersonOneReleaseInfoTo;
            g.PersonOneRelationship = PersonOneRelationship;
            g.NameOfPersonTwoReleaseInfoTo = PersonTwoReleaseInfoTo;
            g.PersonTwoRelationship = PersonTwoRelationship;
            g.AuthorizationFormPolicyHolderSignature = PolicyHolderSignature;
            g.AuthorizationFormPolicyHolderSignatureDate = PolicyHolderSignatureDate;
            g.PersonOneSignature = PersonOneSignature;
            g.PersonOneSignatureDate = PersonOneSignatureDate;
            g.PersonTwoSignature = PersonTwoSignature;
            g.PersonTwoSignatureDate = PersonTwoSignatureDate;

            db.SaveChanges();

            int result = g.GroupHealthInsurance_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //Edit-AuthorizationForm
        public ActionResult EditAuthorizationForm(int? Employee_id, int? GroupHealthInsurance_id)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;

            EmployeeAndInsuranceVM employeeAndInsVM = new EmployeeAndInsuranceVM();

            employeeAndInsVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);
            employeeAndInsVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);

            //if (Employee_id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Group_Health g = db.Group_Health.Find(Employee_id);
            //if (g == null)
            //{
            //    return HttpNotFound();
            //}


            return View(employeeAndInsVM);
        }

        //EditUpdate-AuthorizationForm
        public JsonResult AuthorizationFormEditUpdate(int? Employee_id, int? GroupHealthInsurance_id, string PersonOneReleaseInfoTo, string PersonOneRelationship,
            string PersonTwoReleaseInfoTo, string PersonTwoRelationship, string PolicyHolderSignature, DateTime PolicyHolderSignatureDate,
            string PersonOneSignature, DateTime PersonOneSignatureDate, string PersonTwoSignature, DateTime PersonTwoSignatureDate)
        {

            Group_Health g = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == GroupHealthInsurance_id)
                .Single();

            g.NameOfPersonOneReleaseInfoTo = PersonOneReleaseInfoTo;
            g.PersonOneRelationship = PersonOneRelationship;
            g.NameOfPersonTwoReleaseInfoTo = PersonTwoReleaseInfoTo;
            g.PersonTwoRelationship = PersonTwoRelationship;
            g.AuthorizationFormPolicyHolderSignature = PolicyHolderSignature;
            g.AuthorizationFormPolicyHolderSignatureDate = PolicyHolderSignatureDate;
            g.PersonOneSignature = PersonOneSignature;
            g.PersonOneSignatureDate = PersonOneSignatureDate;
            g.PersonTwoSignature = PersonTwoSignature;
            g.PersonTwoSignatureDate = PersonTwoSignatureDate;

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

                RedirectToAction("EmpOverview", new { g.Employee_id });
            }

            int result = g.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

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

      
    }
}
