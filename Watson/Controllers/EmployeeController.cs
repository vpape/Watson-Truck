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
        private static List<Family_Info> family = new List<Family_Info>();

        public EmployeeController()
        {
     
        }

        //public ActionResult EmpOverview(Employee employee)
        //{
        //    Employee e = db.Employees.Find(employee);

        //    e = employee;

        //    return View(employee);

        //}

        public ActionResult EmpOverview()
        {
            return View(db.Employees.ToList());
        }

        public JsonResult GetEmployee(int Employee_id, string EmpFirstName, string EmpLastName, string EmailAddress)
        {
            var e = db.Employees.Find();
                         
            e.FirstName = EmpFirstName;
            e.LastName = EmpLastName;
            e.EmailAddress = EmailAddress;

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);

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

  
        //EmpEnrollment Method
        //Error with EmployeeRole and Gender
        public JsonResult EmployeeEnrollmentNew(string EmployeeRole, string CurrentEmployer, 
            string JobTitle, string EmpNumber, string MaritalStatus, string FirstName, string LastName,
            DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

            e.EmployeeRole = EmployeeRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            e.MaritalStatus = MaritalStatus;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;    

            db.Employees.Add(e);
            db.SaveChanges();

            //int result = e.Employee_id;

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);
        }

        //EmpContact Method
        public ActionResult EmpContact()
        {
            return View();
        }

        //Error with Employee_id and CityLimits- Undefined
        public JsonResult EmpEnrollmentContact(int Employee_id, string MaritalStatus, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, 
            string PObox2, string City2, string State2, string ZipCode2, string County2, string CityLimits, 
            string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Employee e = new Employee();
            //var e = db.Employees
            //   .Where(i => i.Employee_id == Employee_id)
            //   .Single();

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
            if (CityLimits == "True")
            {
                e.CityLimits = true;
            }
            else
            {
                e.CityLimits = false;
            }
            
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            ViewBag.MaritalStatus = MaritalStatus;

            if (ModelState.IsValid)
            {
                db.Employees.Add(e);

                try
                {
                    db.SaveChanges();

                    if (e.MaritalStatus == "Married")
                    {
                        RedirectToAction("SpEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
                    }
                    //else if (e.MaritalStatus == "MarriedwDep")
                    //{
                    //    RedirectToAction("SpEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
                    //}
                    //else if (e.MaritalStatus == "SinglewDep")
                    //{
                    //    RedirectToAction("DepEnrollment", "Family_Info", new { e.Employee_id, e.MaritalStatus });
                    //}
                    else
                    {
                        RedirectToAction("EnrollmentSelection", "Employee");
                    }
                }

                catch (Exception emp)
                {
                    Console.WriteLine(emp);
                }
            }


            db.Employees.Add(e);
            db.SaveChanges();

            

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);
                          
        }

        //EditEmp Method
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

        public JsonResult EmployeeEditUpdate(int Employee_id, string EmpRole, string CurrentEmployer, string JobTitle, string EmpNumber,
            string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MaritalStatus, 
            string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string PObox2, string City2, string State2, string ZipCode2, 
            string County2, bool CityLimits, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.EmployeeRole = EmpRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
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

            if (ModelState.IsValid)
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;

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


            return Json(new { data = employee, Employee_id }, JsonRequestBehavior.AllowGet);
        }

        //EmpDetail Method
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

        public JsonResult GetEmpDetail(int Employee_id)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            return Json(new { data = e }, JsonRequestBehavior.AllowGet);    
        }

        //DeleteEmp Method
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
        //----------------------------------------------------------------------------------------

        public ActionResult FamilyOverview(/*Family_Info family*/)
        {
            //Family_Info f = db.Family_Info.Find(family);

            //f = family;

            return View(db.Family_Info.ToList());
        }

        public ActionResult FamilyEnrollment()
        {
            return View();
        }

        public JsonResult GetFamilyMember(int FamilyMember_id, int Employee_id, string FirstName, string LastName,
            string RelationshipToInsured, string EmpLastName, string EmpNumber, DateTime DateOfBirth, string MailingAddress,
            string PObox, string City, string State, string County, string ZipCode, string EmailAddress, string PhoneNumber,
            string CellPhone, string Gender, string Employer, string EmployerMailingAddress, string EmployerPObox,
            string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber, bool Medical,
            bool Dental, bool Vision, bool Indemnity)
        {
            var f = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            f.FirstName = FirstName;
            f.LastName = LastName;
            f.RelationshipToInsured = RelationshipToInsured;
            f.LastName = EmpLastName;
            f.SSN = EmpNumber;
            f.DateOfBirth = DateOfBirth;
            f.MailingAddress = MailingAddress;
            f.PObox = PObox;
            f.City = City;
            f.State = State;
            f.ZipCode = ZipCode;
            f.EmailAddress = EmailAddress;
            f.PhoneNumber = PhoneNumber;
            f.CellPhone = CellPhone;
            f.Gender = Gender;
            //f.SSN = 
            f.Employer = Employer;
            f.EmployerMailingAddress = EmployerMailingAddress;
            f.PObox = EmployerPObox;
            f.EmployerCity = EmployerCity;
            f.EmployerState = EmployerState;
            f.EmployerZipCode = EmployerZipCode;
            f.EmployerPhoneNumber = EmployerPhoneNumber;
            f.Medical = Medical;
            f.Dental = Dental;
            f.Vision = Vision;
            f.Indemnity = Indemnity;


            return Json(new { data = f, Employee_id }, JsonRequestBehavior.AllowGet);
        }

        //SpEnrollment Method
        public ActionResult SpEnrollment(string Employee_id, string MaritalStatus)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.MartialStatus = MaritalStatus;

            return View();
        }

        public JsonResult SpEnrollmentNew(int Employee_id, string MaritalStatus, string RelationshipToInsured,
           string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        {
            Family_Info sp = new Family_Info();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;
            sp.Employee_id = Employee_id;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            return Json(new { data = sp, Employee_id }, JsonRequestBehavior.AllowGet);

        }

        //EditSp Method
        public ActionResult EditSp(int Employee_id, int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee_id = Employee_id;

            return View(family);
        }

        public JsonResult SpEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured,
            string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, string PObox2,
            string City2, string State2, string ZipCode2, string County2, string EmailAddress, string PhoneNumber,
            string CellPhone, string Employer, string EmployerAddress, string EmployerPObox, string EmployerCity,
            string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;
            sp.MailingAddress = MailingAddress;
            sp.PObox = PObox;
            sp.City = City;
            sp.State = State;
            sp.ZipCode = ZipCode;
            sp.County = County;
            sp.PhysicalAddress = PhysicalAddress;
            sp.PObox = PObox2;
            sp.City = City2;
            sp.State = State2;
            sp.ZipCode = ZipCode2;
            sp.County = County2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;
            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.PObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            ViewBag.Employee_id = Employee_id;
            ViewBag.MaritalStatus = MaritalStatus;

            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                RedirectToAction("FamilyOverview");
            }

            return Json(new { data = sp }, JsonRequestBehavior.AllowGet);
        }

        //SpDetail Method
        public ActionResult SpDetail(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info family = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            return View(family);
        }

        public JsonResult GetSpDetail(int FamilyMember_id)
        {
            var sp = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            return Json(new { data = sp }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSp(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info family = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }
        
        //DeleteSp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteSp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? FamilyMember_id)
        {
            Family_Info family = db.Family_Info.Find(FamilyMember_id);

            db.DeleteEmployeeAndDependents(FamilyMember_id);

            db.Family_Info.Remove(family);
            db.SaveChanges();

            return RedirectToAction("FamilyOverview");
        }
        //----------------------------------------------------------------------------------------

        public ActionResult DepEnrollment()
        {
            return View();
        }

        public JsonResult DepEnrollmentNew(int Employee_id, string MaritalStatus, string EmpNumber, string RelationshipToInsured,
           string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender, string CoveredByOtherIns,
           string InsCompany, string PolicyNumber, string InsPhoneNumber, string InsMailingAddress, /*string InsPObox,*/
           string InsCity, string InsState, string InsZipCode)
        {
            Family_Info dep = new Family_Info();

            dep.RelationshipToInsured = RelationshipToInsured;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;

            ViewBag.Employee_id = Employee_id;
            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;

            Employee employee = db.Employees.Find(Employee_id);

            if (employee.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (employee.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            Employee emp = new Employee();

            emp.SSN = EmpNumber;

            Other_Insurance o = new Other_Insurance();

            o.CoveredByOtherInsurance = CoveredByOtherIns;
            o.InsuranceCompany = InsCompany;
            o.PolicyNumber = PolicyNumber;
            o.PhoneNumber = InsPhoneNumber;
            o.MailingAddress = InsMailingAddress;
            //o.PObox = InsPObox;
            o.City = InsCity;
            o.State = InsState;
            o.ZipCode = InsZipCode;

            int result = Employee_id;

            db.Family_Info.Add(dep);
            db.Other_Insurance.Add(o);
            db.SaveChanges();

            return Json(new { data = dep, emp, o }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------
    }
}
