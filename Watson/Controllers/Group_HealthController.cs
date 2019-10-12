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

        //show name of dependents, using foreach loop, include add insurance button where you can view and delete insurance
        public ActionResult GrpHealthEnrollment(int? Employee_id, int? GroupHealthInsurance_id)
        {
            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;
            ViewBag.Employee_id = Employee_id;

            GroupHealthGrpHEnrollmentVM groupHGrpHEnrollmentVM = new GroupHealthGrpHEnrollmentVM();

            groupHGrpHEnrollmentVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);   
            groupHGrpHEnrollmentVM.family = db.Family_Info.Where(i=> i.Employee_id == Employee_id).ToList();
            groupHGrpHEnrollmentVM.grpHealth = db.Group_Health.FirstOrDefault(i => i.Employee_id == Employee_id);
            groupHGrpHEnrollmentVM.otherIns = db.Other_Insurance.Where(i => i.Employee_id == Employee_id).ToList();

            
            return View(groupHGrpHEnrollmentVM);

            //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //Create-GrpHealthEnrollment- when saving info below, the objects commented out were making it fail
        public JsonResult GrpHealthEnrollmentNew(int? GroupHealthInsurance_id, int? Employee_id, int? FamilyMember_id, 
            int? OtherInsurance_id, string GroupName, string IMSGroupNumber, string ReasonForGrpCoverageRefusal,
            string OtherCoverage, string OtherReason, string Myself, string Spouse, string Dependent, /*DateTime CafeteriaPlanYear,*/
            string NoneGroupHealthOption, string empOnlyGroupHealthOption, string empSpGroupHealthOption,
            string empDepGroupHealthOption, string empFamGroupHealthOption, string GrpHRefusalEmpSignature, /*DateTime GrpHRefusalEmpSignatureDate,*/
            string GrpHEnrollmentEmpSignature, /*DateTime GrpHEnrollmentEmpSignatureDate,*/ string empDepartment, string empEnrollmentType,
            int empPayroll_id, string empClass, string empJobTitle, /*DateTime empHireDate,*/ /*decimal empAnnualSalary,*/ 
            /*DateTime empEffectiveDate,*/ /*int empHrsWkPerWk,*/ string InsMECPlan, string InsStndPlan, string InsBuyUpPlan, string DentalPlan, 
            string VisionPlan, string spOtherInsCoverage)
        {
           
            //Employee emp = new Employee();
            Employee emp = db.Employees
             .Where(i => i.Employee_id == Employee_id)
             .Single();

            emp.Department = empDepartment;
            emp.EnrollmentType = empEnrollmentType;
            emp.Payroll_id = empPayroll_id;
            emp.Class = empClass;
            //emp.AnnualSalary = empAnnualSalary;
            //emp.EffectiveDate = empEffectiveDate;
            //emp.HoursWorkedPerWeek = empHrsWkPerWk;

            ViewBag.Employee_id = Employee_id;

            Group_Health g = new Group_Health();

            g.GroupName = GroupName;
            g.IMSGroupNumber = IMSGroupNumber;

            g.ReasonForGrpCoverageRefusal = ReasonForGrpCoverageRefusal;
            g.OtherCoverage = OtherCoverage;
            g.OtherReason = OtherReason;
            g.Myself = Myself;
            g.Spouse = Spouse;
            g.Dependent = Dependent;
            //g.CafeteriaPlanYear = CafeteriaPlanYear;
            g.NoMedicalPlan = NoneGroupHealthOption;
            g.EmployeeOnly = empOnlyGroupHealthOption;
            g.EmployeeAndSpouse = empSpGroupHealthOption;
            g.EmployeeAndDependent = empDepGroupHealthOption;
            g.EmployeeAndFamily = empFamGroupHealthOption;
            g.GrpHRefusalEmpSignature = GrpHRefusalEmpSignature;
            //g.GrpHRefusalEmpSignatureDate = GrpHRefusalEmpSignatureDate;
            g.GrpHEnrollmentEmpSignature = GrpHEnrollmentEmpSignature;
            //g.GrpHEnrollmentEmpSignatureDate = GrpHEnrollmentEmpSignatureDate;

            ViewBag.GroupHealthInsurance_id = GroupHealthInsurance_id;

            db.SaveChanges();

            InsurancePlan insPlan = new InsurancePlan();

            insPlan.MECPlan = InsMECPlan;
            insPlan.StandardPlan = InsStndPlan;
            insPlan.BuyUpPlan = InsBuyUpPlan;
            insPlan.DentalPlan = DentalPlan;
            insPlan.VisionPlan = VisionPlan;

            ViewBag.insPlan = insPlan;

            Other_Insurance o = new Other_Insurance();

            o.CoveredByOtherInsurance = spOtherInsCoverage;


            ViewBag.OtherInsurance_id = OtherInsurance_id;

        
            //when saving grouphealth , it fails at this point
            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            ViewBag.sp = sp;

            if (ModelState.IsValid)
            {
                db.Group_Health.Add(g);
                db.InsurancePlans.Add(insPlan);
                db.Other_Insurance.Add(o);
                
                db.SaveChanges();
            }

            int result = g.GroupHealthInsurance_id;
           

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
            DateTime GrpHEnrollmentEmpSignatureDate, string empDepartment, string empEnrollmentType, int empPayroll_id, string empClass,
            string empJobTitle, DateTime empHireDate, decimal empAnnualSalary, DateTime empEffectiveDate, int empHrsWkPerWk, string InsMECPlan,
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
        public JsonResult SalaryRedirectionUpdate(int Employee_id, string MedicalCoverage, string MedicalInsProvider, string EEelectionPreTaxMedIns,
            decimal PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, decimal PremiumPostTaxMedIns, string DentalCoverage, string DentalInsProvider,
            string EEelectionPreTaxDentalIns, decimal PremiumPreTaxDentalIns, string EEelectionPostTaxDentalIns, decimal PremiumPostTaxDentalIns,
            string VisionCoverage, string VisionInsProvider, string EEelectionPreTaxVisionIns, decimal PremiumPreTaxVisionIns,
            string EEelectionPostTaxVisionIns, decimal PremiumPostTaxVisionIns, string AccidentCoverage, string AccidentInsProvider,
            string EEelectionPreTaxAccidentIns, decimal PremiumPreTaxAccidentIns, string EEelectionPostTaxAccidentIns, 
            decimal PremiumPostTaxAccidentIns, string CancerCoverage, string CancerInsProvider, string EEelectionPreTaxCancerIns,
            decimal PremiumPreTaxCancerIns, string EEelectionPostTaxCancerIns, decimal PremiumPostTaxCancerIns, string ShortTermDisabilityCoverage, 
            string StDisabilityProvider, string EEelectionPreTaxStDisability, decimal PremiumPreTaxStDisability, string EEelectionPostTaxStDisability,
            decimal PremiumPostTaxStDisability, string HospitalIndemnityCoverage, string HospitalIndemProvider, string EEelectionPreTaxHospitalIndem,
            decimal PremiumPreTaxHospitalIndem, string EEelectionPostTaxHospitalIndem, decimal PremiumPostTaxHospitalIndem, string TermLifeCoverage, 
            string TermLifeInsProvider, string EEelectionPreTaxTermLifeIns, decimal PremiumPreTaxTermLifeIns, string EEelectionPostTaxTermLifeIns,
            decimal PremiumPostTaxTermLifeIns, string WholeLifeCoverage, string WholeLifeInsProvider, string EEelectionPreTaxWholeLifeIns,
            decimal PremiumPreTaxWholeLifeIns, string EEelectionPostTaxWholeLifeIns, decimal PremiumPostTaxWholeLifeIns, string OtherCoverage,
            string OtherInsProvider, string EEelectionPreTaxOtherIns, decimal PremiumPreTaxOtherIns, string EEelectionPostTaxOtherIns,
            decimal PremiumPostTaxOtherIns, decimal TotalPreTax, decimal TotalPostTax, string empInitials1, string empInitials2, 
            string empInitials3, string empInitials4, string empInitials5, string empSignature, DateTime empSignatureDate)
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

        //Edit-SalaryRedirect
        public ActionResult EditSalaryRedirection(int? Employee_id)
        {
            GroupHealthGrpHEnrollmentVM groupHGrpHEnrollmentVM = new GroupHealthGrpHEnrollmentVM();

            groupHGrpHEnrollmentVM.employee = db.Employees.FirstOrDefault(i => i.Employee_id == Employee_id);

            ViewBag.Employee_id = groupHGrpHEnrollmentVM.employee.Employee_id;

            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(Employee_id);
            if (g == null)
            {
                return HttpNotFound();
            }

            ViewBag.g = g.Employee_id;

            return View(groupHGrpHEnrollmentVM);
        }

        //EditUpdate-SalaryRedirect
        public JsonResult SalaryRedirectionEditUpdate(int Employee_id, int Deductions_id, string MedicalCoverage, string MedicalInsProvider,
            string EEelectionPreTaxMedIns, decimal PremiumPreTaxMedIns, string EEelectionPostTaxMedIns, decimal PremiumPostTaxMedIns,
            string DentalCoverage, string DentalInsProvider, string EEelectionPreTaxDentalIns, decimal PremiumPreTaxDentalIns,
            string EEelectionPostTaxDentalIns, decimal PremiumPostTaxDentalIns, string VisionCoverage, string VisionInsProvider, 
            string EEelectionPreTaxVisionIns, decimal PremiumPreTaxVisionIns, string EEelectionPostTaxVisionIns, decimal PremiumPostTaxVisionIns, 
            string AccidentCoverage, string AccidentInsProvider, string EEelectionPreTaxAccidentIns, decimal PremiumPreTaxAccidentIns, 
            string EEelectionPostTaxAccidentIns, decimal PremiumPostTaxAccidentIns, string CancerCoverage, string CancerInsProvider,
            string EEelectionPreTaxCancerIns, decimal PremiumPreTaxCancerIns, string EEelectionPostTaxCancerIns, decimal PremiumPostTaxCancerIns,
            string ShortTermDisabilityCoverage, string StDisabilityProvider, string EEelectionPreTaxStDisability, decimal PremiumPreTaxStDisability, 
            string EEelectionPostTaxStDisability, decimal PremiumPostTaxStDisability, string HospitalIndemnityCoverage, string HospitalIndemProvider, 
            string EEelectionPreTaxHospitalIndem, decimal PremiumPreTaxHospitalIndem, string EEelectionPostTaxHospitalIndem, 
            decimal PremiumPostTaxHospitalIndem, string TermLifeCoverage, string TermLifeInsProvider, string EEelectionPreTaxTermLifeIns, 
            decimal PremiumPreTaxTermLifeIns, string EEelectionPostTaxTermLifeIns, decimal PremiumPostTaxTermLifeIns, string WholeLifeCoverage, 
            string WholeLifeInsProvider, string EEelectionPreTaxWholeLifeIns, decimal PremiumPreTaxWholeLifeIns, string EEelectionPostTaxWholeLifeIns,
            decimal PremiumPostTaxWholeLifeIns, string OtherCoverage, string OtherInsProvider, string EEelectionPreTaxOtherIns,
            decimal PremiumPreTaxOtherIns, string EEelectionPostTaxOtherIns, decimal PremiumPostTaxOtherIns, decimal TotalPreTax, 
            decimal TotalPostTax, string empInitials1, string empInitials2, string empInitials3, string empInitials4, 
            string empInitials5, string empSignature, DateTime empSignatureDate)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            ViewBag.e = e;

            Deduction d = db.Deductions
                .Where(i => i.Deductions_id == Deductions_id)
                .Single();

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
            //THere are 5 items that need initials for the salaryRedirect waviers.

            ViewBag.d = d;

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

            int result = d.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //SalaryRedirect-End----------------------------------------------------------------------------------

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
            string PersonTwoReleaseInfoTo, string PersonTwoRelationship, string PolicyHolderSignature, DateTime PolicyHolderSignatureDate,
            string PersonOneSignature, DateTime PersonOneSignatureDate, string PersonTwoSignature, DateTime PersonTwoSignatureDate)
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

            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group_Health g = db.Group_Health.Find(Employee_id);
            if (g == null)
            {
                return HttpNotFound();
            }


            return View(employeeAndInsVM);
        }

        //EditUpdate-AuthorizationForm
        public JsonResult AuthorizationFormEditUpdate(int Employee_id, int GroupHealthInsurance_id, string PersonOneReleaseInfoTo, string PersonOneRelationship,
            string PersonTwoReleaseInfoTo, string PersonTwoRelationship, string PolicyHolderSignature, DateTime PolicyHolderSignatureDate,
            string PersonOneSignature, DateTime PersonOneSignatureDate, string PersonTwoSignature, DateTime PersonTwoSignatureDate)
        {
            Employee e = db.Employees
               .Where(i => i.Employee_id == Employee_id)
               .Single();

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

        //AuthorizationForm-End-----------------------------------------------------------------------------

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

        //LifeInsurance-Start-----------------------------------------------------------------------------

        public ActionResult LifeInsuranceEnrollment()
        {
            return View();
        }

        //Create-LifeIns
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

        //Edit-LifeIns
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

        //EditUpdate-LifeIns
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

        //LifeInsurance-End-----------------------------------------------------------------------------
    }
}
