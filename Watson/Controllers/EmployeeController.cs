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
    //[Authorize]
    public class EmployeeController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();
        private static List<Employee> employee = new List<Employee>();
        private static List<Family_Info> family = new List<Family_Info>();
        private static List<Other_Insurance> otherins = new List<Other_Insurance>();

        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeController()
        {
     
        }

        //EmpOverview
        public ActionResult EmpOverview(int? Employee_id)
        {
            ViewBag.Employee_id = Employee_id;

            if (Employee_id == null)
            {
                //return View(db.Employees.Find(Employee_id));
                return View(db.Employees.ToList());
            }
            else
            {
                return View(db.Employees.Find(Employee_id));
            }
        }

        //----------------------------------------------------------------------------------------

        public ActionResult NewEnrollmentSelection()
        {
            return View();
        }

        public ActionResult EnrollmentSelection()
        {
            return View();
        }

        public ActionResult NewEmployeeEnrollment()
        {
            return View();
        }
 
        //Create-EmpEnrollment
        public JsonResult EmployeeEnrollmentNew(string Role, string CurrentEmployer, string JobTitle, string EmpNumber,
            DateTime HireDate, string MaritalStatus, string FirstName, string LastName, DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

            e.EmployeeRole = Role;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            e.HireDate = HireDate;
            e.MaritalStatus = MaritalStatus;
            e.FirstName = FirstName;
            e.LastName = LastName;
            e.DateOfBirth = DateOfBirth;
            e.Gender = Gender;

            ViewBag.Employee_id = e.Employee_id;

            db.Employees.Add(e);
            db.SaveChanges();

            int result = e.Employee_id;

            return Json( new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //EmpContact Method
        //public ActionResult EmpContact()
        //{
        //    return View();
        //}

        //Create-EmpContact
        public JsonResult EmpEnrollmentContact(int Employee_id, string MaritalStatus, string MailingAddress, string PObox, 
            string City, string State, string ZipCode, string County, string CityLimits, string PhysicalAddress,
            string City2, string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Employee e = db.Employees
               .Where(i => i.Employee_id == Employee_id)
               .Single();

            e.MailingAddress = MailingAddress;
            e.PObox = PObox;
            e.City = City;
            e.State = State;
            e.ZipCode = ZipCode;
            e.County = County;
            e.CityLimits = CityLimits;
            e.PhysicalAddress = PhysicalAddress;
            e.CityTwo = City2;
            e.StateTwo = State2;
            e.ZipCodeTwo = ZipCode2; 
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            ViewBag.MaritalStatus = e.MaritalStatus;
            ViewBag.Employee_id = e.Employee_id;

            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();

                    //if (e.MaritalStatus == "Married")
                    //{
                    //     RedirectToAction("SpEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                    //     RedirectToAction("SpEnrollment");
                    //}
                    //else if (e.MaritalStatus == "MarriedwDep")
                    //{
                    //     RedirectToAction("SpEnrollment", new { Employee_id = e.Employee_id, MaritalStatus = e.MaritalStatus });
                    //     RedirectToAction("SpEnrollment", "Employee", new { Employee_id = e.Employee_id, MaritalStatus = e.MaritalStatus });
                    //}
                    //else if (e.MaritalStatus == "SinglewDep")
                    //{
                    //     RedirectToAction("SpEnrollment", "Employee");
                    //     RedirectToAction("DepEnrollment", "Employee", new { e.Employee_id, e.MaritalStatus });
                    //}
                    //else
                    //{
                    //     RedirectToAction("EnrollmentSelection", "Employee", new { e.Employee_id, e.MaritalStatus });
                    //}
                }

                catch (Exception emp)
                {
                    Console.WriteLine(emp);
                }
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);             
        }

        //Edit-Emp
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

            ViewBag.Employee_id = e.Employee_id;

            return View(e);
        }

        //EditUpdate-Emp
        public JsonResult EmployeeEditUpdate(int Employee_id,string EmpRole, string CurrentEmployer, string JobTitle, 
            string EmpNumber, DateTime HireDate, string FirstName, string LastName, DateTime DateOfBirth, string Gender,
            string MaritalStatus, string MailingAddress, string PObox, string City, string State, string ZipCode, string County,
            string PhysicalAddress, string City2, string State2, string ZipCode2, string CityLimits, string EmailAddress, 
            string PhoneNumber, string CellPhone)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.EmployeeRole = EmpRole;
            e.CurrentEmployer = CurrentEmployer;
            e.JobTitle = JobTitle;
            e.SSN = EmpNumber;
            e.HireDate = HireDate;
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
            e.CityTwo = City2;
            e.StateTwo = State2;
            e.ZipCodeTwo = ZipCode2;
            e.CityLimits = CityLimits;
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            ViewBag.Employee_id = e.Employee_id;

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

                RedirectToAction("EmpOverview", new { e.Employee_id });
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Get-EmpDetail
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

            ViewBag.Employee_id = Employee_id;

            return View(e);
        }

        //Get-EmpDetail
        public JsonResult GetEmpDetail(int Employee_id, string City2, string State2, string ZipCode2)
        {
            ViewBag.Employee_id = Employee_id;

            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

            e.CityTwo = City2;
            e.StateTwo = State2;
            e.ZipCodeTwo = ZipCode2;

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);    
        }
        //----------------------------------------------------------------------------------------

        //DeleteEmp Method
        public ActionResult DeleteEmp(int? Employee_id)
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

        //DeleteEmp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Employee_id)
        {
            Employee e = db.Employees.Find(Employee_id);
            db.Employees.Remove(e);
            db.SaveChanges();

            db.DeleteEmployeeAndDependents(Employee_id);

            return RedirectToAction("EmpOverview", new { e.Employee_id });
        }

        //----------------------------------------------------------------------------------------

        public ActionResult FamilyOverview(int? Employee_id)
        {
            ViewBag.Employee_id = Employee_id;

            var familyInfo = (from fi in db.Family_Info
                              where fi.Employee_id == Employee_id
                              select fi).ToList();

            return View(familyInfo);
        }

        public ActionResult NewFamilyEnrollment()
        {
            return View();
        }

        public ActionResult FamilyEnrollment(int? Employee_id, string MaritalStatus)
        {
            ViewBag.Employee_id = Employee_id;

            Employee e = db.Employees.Find(Employee_id);
            //if (e.MaritalStatus == "Single")
            //{
            //    ViewBag.spouseExist = false;
            //    ViewBag.MaritalStatus = "Single";
            //}
            //else if (e.MaritalStatus == "SinglewDep")
            //{
            //    ViewBag.spouseExist = false;
            //    ViewBag.MaritalStatus = "SinglewDep";
            //}
            //else if (e.MaritalStatus == "Married")
            //{
            //    ViewBag.spouseExist = true;
            //    ViewBag.MaritalStatus = "Married";
            //}
            //else
            //{
            //    ViewBag.spouseExist = true;
            //    ViewBag.MaritalStatus = "MarriedwDep";
            //}


            return View();
        }
        
        //----------------------------------------------------------------------------------------

        public ActionResult NewSpouseEnrollment(int? Employee_id)
        {
            ViewBag.Employee_id = Employee_id;

            return View();
        }

        //SpEnrollment Method
        public ActionResult SpEnrollment(int Employee_id, string MaritalStatus, string RelationshipToInsured)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.MaritalStatus = MaritalStatus;
            ViewBag.RelationshipToInsured = RelationshipToInsured= "Spouse";

            return View();
        }

        //Create-SpouseEnrollment
        public JsonResult SpEnrollmentNew(int Employee_id, string RelationshipToInsured, string FirstName, string LastName, 
            DateTime DateOfBirth, string Gender)
        {
            //Create an if statement that checks to see if there's already a record

            //var output = (from fi in db.Family_Info
            //              where fi.FamilyMember_id == FamilyMember_id
            //              select fi).SingleOrDefault();

            //if(output != null)
            //{

            //}

            Employee e = db.Employees.Find(Employee_id);
            ViewBag.MaritalStatus = e.MaritalStatus;

            ViewBag.Employee_id = Employee_id;

            Family_Info sp = new Family_Info();

            sp.Employee_id = Employee_id;
            sp.RelationshipToInsured = RelationshipToInsured;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;
            

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            int result = sp.FamilyMember_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //SpContact Method
        //public ActionResult SpContact(int Employee_id)

        //Create-SpouseContact
        public JsonResult SpEnrollmentContact(int FamilyMember_id, int Employee_id, string MailingAddress, string PObox, string City,
            string State, string ZipCode, string County, string PhysicalAddress, string City2, string State2, string ZipCode2, 
            string EmailAddress, string PhoneNumber, string CellPhone)
        {
            //Family_Info sp = new Family_Info();

            var sp = (from fi in db.Family_Info
                     where fi.FamilyMember_id == FamilyMember_id
                     select fi).SingleOrDefault();

            //sp.Employee_id = Employee_id;
            sp.MailingAddress = MailingAddress;
            sp.PObox = PObox;
            sp.City = City;
            sp.State = State;
            sp.ZipCode = ZipCode;
            sp.County = County;
            sp.PhysicalAddress = PhysicalAddress;
            sp.CityTwo = City2;
            sp.StateTwo = State2;
            sp.ZipCodeTwo = ZipCode2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;

            //db.Family_Info.Add(sp);
            db.SaveChanges();
            
            int result = sp.FamilyMember_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //SpEmployment Method
        //public ActionResult SpEmployment(int? Employee_id, string MaritalStatus)


        //Create-SpouseEmployment
        public JsonResult SpEnrollmentEmployment(int? FamilyMember_id, int Employee_id, string MaritalStatus, string Employer, string EmployerAddress,
            string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            //Family_Info sp = new Family_Info();

            ViewBag.Employee_id = Employee_id;
            ViewBag.MaritalStatus = MaritalStatus;

            var sp = (from fi in db.Family_Info
                      where fi.FamilyMember_id == FamilyMember_id
                      select fi).SingleOrDefault();

            sp.Employee_id = Employee_id;
            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerPObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            if (ModelState.IsValid)
            {
                //db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            int result = sp.FamilyMember_id;

            //if (MaritalStatus == "MarriedwDep")
            //{
            //    return RedirectToAction("DepEnrollment", "Family_info", new { sp.Employee_id, sp.MaritalStatus });
            //}

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------

        //EditSp Method
        public ActionResult EditSp(int Employee_id, int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (f == null)
            {
                return HttpNotFound();
            }

            ViewBag.Employee_id = f.Employee_id;

            return View(f);
        }

        //EditUpdate-Spouse
        public JsonResult SpEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured,
            string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, string Gender, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress, string City2, 
            string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone, string Employer, 
            string EmployerAddress, string EmployerPObox, string EmployerCity, string EmployerState, string EmployerZipCode,
            string EmployerPhoneNumber)
        {
            var sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.RelationshipToInsured == "Spouse")
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
            sp.CityTwo = City2;
            sp.StateTwo = State2;
            sp.ZipCodeTwo = ZipCode2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;
            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerPObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            ViewBag.FamilyMember_id = sp.FamilyMember_id;
            ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;
            ViewBag.RelationshipToInsured = "Spouse";

            Employee e = db.Employees.Find(Employee_id);
            if (e.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (e.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }

                RedirectToAction("FamilyOverview", new { sp.Employee_id });
            }

            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Get-SpDetail
        public ActionResult SpDetail(int Employee_id, int? FamilyMember_id, string MaritalStatus)
        {
            ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.FamilyMember_id = f.FamilyMember_id;

            return View(f);
        }

        //Get-SpDetail
        public JsonResult GetSpDetail(int FamilyMember_id)
        {
            var sp = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            ViewBag.FamilyMember_id = sp.FamilyMember_id;

            int result = sp.FamilyMember_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //DeleteSp Method
        public ActionResult DeleteSp(int Employee_id, int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info sp = db.Family_Info.Find(FamilyMember_id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.sp = sp.FamilyMember_id;
            return View(sp);
        }
        
        //DeleteSp Method
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteSp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? FamilyMember_id)
        {
            Family_Info sp = db.Family_Info.Find(FamilyMember_id);

            Other_Insurance other = db.Other_Insurance.Find(FamilyMember_id);

            //db.DeleteSpouseAndDependent(FamilyMember_id);

            db.Family_Info.Remove(sp);
            db.Other_Insurance.Remove(other);
            db.SaveChanges();

            return RedirectToAction("FamilyOverview", new { sp.Employee_id });
        }
        //----------------------------------------------------------------------------------------

        public ActionResult NewDependentEnrollment()
        {
            return View();
        }

        public ActionResult DepEnrollment(int Employee_id, string RelationshipToInsured)
        {
            ViewBag.Employee_id = Employee_id;
            ViewBag.RelationshipToInsured = RelationshipToInsured = "Dependent";

            return View();
        }

        //Create-DepEnrollment
        public JsonResult DepEnrollmentNew(int Employee_id, string RelationshipToInsured,
            string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender, string EmpNumber)
        {
            Family_Info dep = new Family_Info();

            dep.Employee_id = Employee_id;
            dep.RelationshipToInsured = RelationshipToInsured;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;

            ViewBag.spouseExist = true;
            ViewBag.RelationshipToInsured = "Dependent";

            Employee e = db.Employees.Find(Employee_id);
            if (e.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (e.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            int result = dep.Employee_id;

            db.Family_Info.Add(dep);
           
            db.SaveChanges();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //EditDep Method
        public ActionResult EditDep(int Employee_id, int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (f == null)
            {
                return HttpNotFound();
            }

            ViewBag.Family_Info = f.Employee_id;

            return View(f);
        }

        //EditUpdate-DepEdit
        public JsonResult DepEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, string RelationshipToInsured, 
            string DepFirstName, string DepLastName, DateTime DateOfBirth, string Gender, string EmpNumber)
        {
            Family_Info dep = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.RelationshipToInsured == "Dependent")
                .Single();

            dep.RelationshipToInsured = RelationshipToInsured;
            dep.FirstName = DepFirstName;
            dep.LastName = DepLastName;
            dep.DateOfBirth = DateOfBirth;
            dep.Gender = Gender;

            ViewBag.spouseExist = true;
            ViewBag.MartialStatus = MaritalStatus;

            Employee e = db.Employees.Find(Employee_id);
            if (e.MaritalStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (e.MaritalStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            var emp = new Employee()
            {
                SSN = EmpNumber
            };
            ViewBag.EmpNumber = emp;

            int result = dep.Employee_id;

            if (ModelState.IsValid)
            {
                db.Entry(dep).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }

                RedirectToAction("FamilyOverview", new { e.Employee_id });
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //Get-DepDetail
        public ActionResult DepDetail(int? FamilyMember_id)
        {
            //ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info dep = db.Family_Info.Find(FamilyMember_id);
            if (dep == null)
            {
                return HttpNotFound();
            }

            return View(dep);
        }

        //Get-DepDetail
        public JsonResult GetDepDetail(int FamilyMember_id)
        {
            var dep = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            int result = dep.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //DeleteDep Method
        public ActionResult DeleteDep(int? FamilyMember_id)
        {
            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family_Info dep = db.Family_Info.Find(FamilyMember_id);
            if (dep == null)
            {
                return HttpNotFound();
            }

            return View(dep);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("DeleteDep")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? FamilyMember_id)
        {
            Family_Info dep = db.Family_Info.Find(FamilyMember_id);

            db.DeleteEmployeeAndDependents(FamilyMember_id);

            db.Family_Info.Remove(dep);
            db.SaveChanges();

            return RedirectToAction("FamilyOverview", new { dep.Employee_id });
        }
        //----------------------------------------------------------------------------------------
    }
}
