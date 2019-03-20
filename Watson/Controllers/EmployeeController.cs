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
    //[Authorize]
    public class EmployeeController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();
        
        private static List<Employee> employee = new List<Employee>();
        
        public EmployeeController()
        {
     
        }

        public ActionResult EmpOverview(Employee employee)
        {
            Employee e = db.Employees.Find(employee);

            e = employee;

            return View(employee);

        }

        public JsonResult GetEmployee(int Employee_id, string EmployeeNumber, string FirstName, string LastName, 
            string JobTitle, string MailingAddress, string City, string State, string ZipCode)
        {
            //Employee e = new Employee();
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.SSN = EmployeeNumber;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.JobTitle = JobTitle;
            e.MailingAddress = MailingAddress;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //----------------------------------------------------------------------------------------

        public ActionResult EnrollmentSelection()
        {
            return View();
        }

        public ActionResult NewEmployeeEnrollment()
        {
            return View();
        }

        public JsonResult EmployeeEnrollmentNew(int Employee_id, string EmployeeRole, string CurrentEmployer, 
            string JobTitle, string EmployeeNumber, string MaritalStatus, string FirstName, string LastName,
            DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

            e.EmployeeRole = EmployeeRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmployeeNumber;
            e.MaritalStatus = MaritalStatus;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            // redirect to specifc page based on MaritalStatus selected 
            //    if (ModelState.IsValid)
            //    {
            //        db.Employees.Add(employee);

            //        try
            //        {
            //            db.SaveChanges();

            //            Redirect is based on marital status, which it's not working
            //            if (employee.MaritalStatus == "Married")
            //            {
            //                return RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
            //            }
            //            else if (employee.MaritalStatus == "MarriedwDep")
            //            {
            //                return RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
            //            }
            //            else if (employee.MaritalStatus == "SinglewDep")
            //            {
            //                return RedirectToAction("DependentEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
            //            }
            //            else
            //            {
            //                return RedirectToAction("EmployeeOverview", "Employee");
            //            }
            //        }

            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //        }
            //    }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //---------------------------------------------------------------------------------------- 

        //changed Contact() to:
        public ActionResult EmpContact()
        {
            return View();
        }

        // changed EmployeeEnrollmentAddress(int Employee_id, string MailingAddress, string City) to:
        public JsonResult EmpEnrollmentContact(int Employee_id, string MailingAddress, string PObox, string City,
            string State, string ZipCode, string County, string PhysicalAddress, string PObox2, string City2,
            string State2, string ZipCode2, string County2, bool CityLimits, string EmailAddress, string PhoneNumber,
            string CellPhone)
        {
            var e = db.Employees
                    .Where(i => i.Employee_id == Employee_id)
                    .Single();
                                
            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.PhysicalAddress = PhysicalAddress;
            e.PObox = PObox2;
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;
            e.County = County2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
                          
        }

        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult EmployeeEnrollmentContactUpdate(int e_id)
        //{
        //    Employee e = db.Employees
        //        .Where(i => i.Employee_id == e_id)
        //        .SingleOrDefault();

        //    db.Employees.Add(e);
        //    db.SaveChanges();
            
        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        //}

        //----------------------------------------------------------------------------------------

        public ActionResult EditEmp(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        public JsonResult EmployeeEditUpdate(int Employee_id, string CurrentEmployer, string JobTitle, string EmployeeNumber,
            string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MaritalStatus, 
            string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string PObox2, string City2, string State2, string ZipCode2, 
            string County2, bool CityLimits, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();
                //.SingleOrDefault()

            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmployeeNumber;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;
            e.MaritalStatus = MaritalStatus;
            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.PhysicalAddress = PhysicalAddress;
            e.PObox = PObox2;
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;
            e.County = County2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            int result = Employee_id;

            if (ModelState.IsValid)
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EmpDetail(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(Employee_id);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        public JsonResult GetEmpDetail(int Employee_id, string CurrentEmployer, string JobTitle, string EmployeeNumber,
            string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MaritalStatus,
            string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string PObox2, string City2, string State2, string ZipCode2, 
            string County2, bool CityLimits, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmployeeNumber;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;
            e.MaritalStatus = MaritalStatus;
            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.PhysicalAddress = PhysicalAddress;
            e.PObox = PObox2;
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;
            e.County = County2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            int result = Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);    
        }

        //----------------------------------------------------------------------------------------

        public ActionResult EmpInsurance()
        {         
            return View();
        }

        public ActionResult GrpHealthEnrollment()
        {
            //Employee emp = db.Employees.Find(e_id);
            //Group_Health enrollment = new Group_Health();

            //enrollment.GroupHealthInsurance_id = grpH_id;

            //return View(enrollment);
            return View();
        }

        public JsonResult GetGrpHealth(int grpH_id)
        {
            var output = from g in db.Group_Health
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
                         };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GrpHealthUpdate(int grpH_id, int e_id)
        //{
        //    Group_Health grpH = db.Group_Health
        //        .Where(i => i.GroupHealthInsurance_id == grpH_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .SingleOrDefault();

        //    db.Group_Health.Add(grpH);
        //    db.SaveChanges();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        //}
        //----------------------------------------------------------------------------------------

        public ActionResult LifeInsEnrollment(int lifeIns_id)
        {
            //Employee emp = db.Employees.Find(e_id);
            //Life_Insurance lifeIns = db.Life_Insurance.Find(lifeIns_id);
            Life_Insurance lifeIns = new Life_Insurance();

            lifeIns.LifeInsurance_id = lifeIns_id;

            return View(lifeIns); 
        }

        public JsonResult GetLifeIns(int lifeIns_id)
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

        //public JsonResult LifeInsUpdate(int lifeIns_id, int e_id)
        //{
        //    Life_Insurance lifeIns = db.Life_Insurance
        //        .Where(i => i.LifeInsurance_id == lifeIns_id)
        //        .Where(i => i.Employee_id == e_id)
        //        .SingleOrDefault();

        //    db.Life_Insurance.Add(lifeIns);
        //    db.SaveChanges();

        //    return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult EditLifeIns(int? lifeIns_id)
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
    
        public JsonResult EditLifeInsurance(int lifeIns_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                          {
                             e.LifeInsurance_id,
                             e.Employee_id,
                              
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LifeInsEditUpdate(int lifeIns_id, int e_id)
        {
            Life_Insurance lifeIns = db.Life_Insurance
                .Where(i => i.LifeInsurance_id == lifeIns_id)
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(lifeIns).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
       
        public ActionResult DeleteEmp(int? Employee_id)
        {
            if (Employee_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(Employee_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);           
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Employee_id)
        {
            Employee employee = db.Employees.Find(Employee_id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(Employee_id);

            return RedirectToAction("EmpOverview");
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
