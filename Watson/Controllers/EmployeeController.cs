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

        //changed from int e_id
        public ActionResult EmployeeOverview(Employee emp)
        {
            Employee employee = db.Employees.Find(emp);

            employee = emp;

            return View(emp);

            //Employee emp = db.Employees.Find(e_id);
            //emp.Employee_id = e_id;
            //return View(db.Employees.ToList());

        }


        public JsonResult GetEmployee(int e_id)
        {
            var output = from e in db.Employees
                        where e.Employee_id == e_id
                        select new
                        {
                            e.Employee_id,
                            e.SSN,
                            e.FirstName,
                            e.LastName,
                            e.JobTitle,
                            e.MailingAddress,
                            e.City,
                            e.State,
                            e.ZipCode,

                        };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .FirstOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
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

        //public ActionResult EmployeeEnrollment()
        //{
        //    Employee emp = new Employee();

        //    return View(emp);
        //}

        //[System.Web.Mvc.HttpPost]
        //public ActionResult EmployeeEnrollment(Employee emp)
        //{
        //    Employee newEmp = new Employee();

        //    newEmp = emp;

        //    //emp.Employee_id = Employee_id;

        //    return View(newEmp.Employee_id);
        //}

        public JsonResult EmployeeEnrollmentNew(string CurrentEmployer, string JobTitle, string EmployeeNumber, string MaritalStatus, string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

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

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeEnrollmentUpdate(int e_id)
        {
            Employee employee = db.Employees
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);

                try
                {
                    db.SaveChanges();

                    //Redirect is based on marital status, which it's not working
                    //if (employee.MaritalStatus == "Married")
                    //{
                    //    return RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
                    //}
                    //else if (employee.MaritalStatus == "MarriedwDep")
                    //{
                    //    return RedirectToAction("SpouseEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
                    //}
                    //else if (employee.MaritalStatus == "SinglewDep")
                    //{
                    //    return RedirectToAction("DependentEnrollment", "Family_Info", new { employee.Employee_id, employee.MaritalStatus });
                    //}
                    //else
                    //{
                    //    return RedirectToAction("EmployeeOverview", "Employee");
                    //}
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //---------------------------------------------------------------------------------------- 
        public ActionResult Contact()
        {
            return View();
        }

        //use code below for EmployeeEnrollmentContact
        //public JsonResult EmployeeEnrollmentAddress(int Employee_id, string MailingAddress, string City)
        //{
        //    var e = db.Employees
        //            .Where(i => i.Employee_id == Employee_id)
        //            .Single();

        //    e.MailingAddress = MailingAddress;
        //    e.City = City;

        //    db.SaveChanges();


        //    int result = e.Employee_id;

        //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult EmployeeEnrollmentContact(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                              e.Employee_id,
                              e.MailingAddress,
                              e.PhysicalAddress,
                              e.PObox,
                              e.City,
                              e.State,
                              e.ZipCode,
                              e.County,
                              e.CityLimits,
                              e.EmailAddress,
                              e.PhoneNumber,
                              e.CellPhone,
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
                          
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeEnrollmentContactUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            db.Employees.Add(e);
            db.SaveChanges();
            
            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult Edit(int? e_id)
        {
            if (e_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(e_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public JsonResult GetEmployeeEdit(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Gender,
                              e.MaritalStatus,
                              e.MailingAddress,
                              e.PhysicalAddress,
                              e.PObox,
                              e.City,
                              e.State,
                              e.ZipCode,
                              e.County,
                              e.CityLimits,
                              e.EmailAddress,
                              e.PhoneNumber,
                              e.CellPhone,
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmployeeEditUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }            

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        public ActionResult Detail(int? e_id)
        {
            if (e_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(e_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public JsonResult GetDetail(int e_id)
        {
            var output = from e in db.Employees
                          where e.Employee_id == e_id
                          select new
                          {
                              e.Employee_id,
                              e.CurrentEmployer,
                              e.JobTitle,
                              e.SSN,
                              e.FirstName,
                              e.LastName,
                              e.DateOfBirth,
                              e.Gender,
                              e.MaritalStatus,
                              e.MailingAddress,
                              e.PhysicalAddress,
                              e.PObox,
                              e.City,
                              e.State,
                              e.ZipCode,
                              e.County,
                              e.CityLimits,
                              e.EmailAddress,
                              e.PhoneNumber,
                              e.CellPhone,
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);    
        }

        public JsonResult DetailUpdate(int e_id)
        {
            Employee e = db.Employees
                .Where(i => i.Employee_id == e_id)
                .FirstOrDefault();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //GET: api/Employee/5
        //[System.Web.Http.Route("api/Employee/Insurance/{User_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Employee Insurance(int? id)
        //{
        //    return employee.Where(e => e.User_id == id).FirstOrDefault();
        //}

        // POST: api/Employee
        //[System.Web.Http.Route("api/Employee/Insurance")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Insurance([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
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

        public ActionResult EmployeeInsurance()
        {         
            return View();
        }

        //int grpH_id
        public ActionResult GroupHealthEnrollment()
        {
            //Employee emp = db.Employees.Find(e_id);
            //Group_Health enrollment = new Group_Health();

            //enrollment.GroupHealthInsurance_id = grpH_id;

            //return View(enrollment);
            return View();
        }

        public JsonResult GetGroupHealth(int grpH_id)
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

        public JsonResult GroupHealthUpdate(int grpH_id, int e_id)
        {
            Group_Health grpH = db.Group_Health
                .Where(i => i.GroupHealthInsurance_id == grpH_id)
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            db.Group_Health.Add(grpH);
            db.SaveChanges();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------
        public ActionResult LifeInsuranceEnrollment(int lifeIns_id)
        {
            //Employee emp = db.Employees.Find(e_id);
            //Life_Insurance lifeIns = db.Life_Insurance.Find(lifeIns_id);
            Life_Insurance lifeIns = new Life_Insurance();

            lifeIns.LifeInsurance_id = lifeIns_id;

            return View(lifeIns); 
        }

        public JsonResult GetLifeInsurance(int lifeIns_id)
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

        public JsonResult LifeInsuranceUpdate(int lifeIns_id, int e_id)
        {
            Life_Insurance lifeIns = db.Life_Insurance
                .Where(i => i.LifeInsurance_id == lifeIns_id)
                .Where(i => i.Employee_id == e_id)
                .SingleOrDefault();

            db.Life_Insurance.Add(lifeIns);
            db.SaveChanges();

            return Json(new { data = "success" }, JsonRequestBehavior.AllowGet);
        }

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
    
        public JsonResult GetEditLifeInsurance(int lifeIns_id)
        {
            var output = from e in db.Life_Insurance
                         select new
                          {
                             e.LifeInsurance_id,
                             e.Employee_id,
                              
                          };

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LifeInsuranceEditUpdate(int lifeIns_id, int e_id)
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
       
        public ActionResult Delete(int? e_id)
        {
            if (e_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(e_id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);           
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int e_id)
        {
            Employee employee = db.Employees.Find(e_id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(e_id);

            return RedirectToAction("EmployeeOverview");
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
