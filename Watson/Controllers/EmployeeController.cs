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
        private static List<Other_Insurance> otherins = new List<Other_Insurance>();

        public EmployeeController()
        {
     
        }

        //public ActionResult EmpOverview(Employee employee)
        //{
        //    Employee e = db.Employees.Find(employee);

        //    e = employee;

        //    return View(employee);

        //}

        public ActionResult EmpOverview(int? Employee_id)
        {
            if(Employee_id == null)
            {
                return View(db.Employees.ToList());
            }
            else
            {
                return View(db.Employees.Find(Employee_id));
            }
            
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

        public ActionResult EnrollmentSelection(string MaritalStatus)
        {
            var empMaritalStatus = new Employee()
            {
                MaritalStatus = "Single"
            };
            ViewBag.MaritalStatus = empMaritalStatus;

            return View(empMaritalStatus);
        }

        public ActionResult NewEmployeeEnrollment()
        {
            return View();
        }

  
        //EmpEnrollment Method
        public JsonResult EmployeeEnrollmentNew(string Role, string CurrentEmployer, 
            string JobTitle, string EmpNumber, string MaritalStatus, string FirstName, string LastName,
            DateTime DateOfBirth, string Gender)
        {
            Employee e = new Employee();

            e.EmployeeRole = Role;
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

            int result = e.Employee_id;

            return Json( new { data = result }, JsonRequestBehavior.DenyGet );
        }

        //EmpContact Method
        public ActionResult EmpContact()
        {
            return View();
        }

        
        public JsonResult EmpEnrollmentContact(int Employee_id, string MaritalStatus, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string CityLimits,
            string PhysicalAddress, string City2, string State2, string ZipCode2,
            string EmailAddress, string PhoneNumber, string CellPhone)
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
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2; 
            e.EmailAddress = EmailAddress;
            e.PhoneNumber = PhoneNumber;
            e.CellPhone = CellPhone;

            ViewBag.MaritalStatus = e.MaritalStatus;

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

            ViewBag.Employee = e.Employee_id;

            return View(e);
        }

        public JsonResult EmployeeEditUpdate(int Employee_id,string EmpRole, string CurrentEmployer, 
            string JobTitle, string EmpNumber, string FirstName, string LastName, DateTime DateOfBirth, 
            string Gender, string MaritalStatus, string MailingAddress, string PObox, string City, 
            string State, string ZipCode, string County, string PhysicalAddress, string City2,
            string State2, string ZipCode2, string CityLimits, string EmailAddress, 
            string PhoneNumber, string CellPhone)
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
            e.City = City2;
            e.State = State2;
            e.ZipCode = ZipCode2;
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

                RedirectToAction("EmpOverview", new { e.Employee_id });
            }

            int result = e.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
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

            ViewBag.Employee = e;

            return View(e);
        }

        public JsonResult GetEmpDetail(int Employee_id)
        {
            var e = db.Employees
                .Where(i => i.Employee_id == Employee_id)
                .Single();

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

        public JsonResult GetFamilyMember(int Employee_id, int FamilyMember_id, string FirstName, 
            string LastName, string RelationshipToInsured, string EmpLastName, string EmpNumber, 
            DateTime DateOfBirth, string MailingAddress, string PObox, string City, string State, 
            string County, string ZipCode, string EmailAddress, string PhoneNumber, string CellPhone,
            string Gender, string Employer, string EmployerMailingAddress, string EmployerPObox,
            string EmployerCity, string EmployerState, string EmployerZipCode, string EmployerPhoneNumber, 
            bool Medical, bool Dental, bool Vision, bool Indemnity)
        {
            var f = db.Family_Info
                .Where(i => i.Employee_id == Employee_id)
                .Where(i => i.FamilyMember_id == FamilyMember_id)
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
            //f.SSN = SSN;
            f.Employer = Employer;
            f.EmployerMailingAddress = EmployerMailingAddress;
            f.EmployerPObox = EmployerPObox;
            f.EmployerCity = EmployerCity;
            f.EmployerState = EmployerState;
            f.EmployerZipCode = EmployerZipCode;
            f.EmployerPhoneNumber = EmployerPhoneNumber;
            f.Medical = Medical;
            f.Dental = Dental;
            f.Vision = Vision;
            f.Indemnity = Indemnity;

            int result = f.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //----------------------------------------------------------------------------------------

        //SpEnrollment Method
        public ActionResult SpEnrollment()
        {
            return View();
        }

        //SpEnrollment2 Method- Test no layout
        public ActionResult SpEnrollment2()
        {
            return View();
        }

        public JsonResult SpEnrollmentNew(int Employee_id, int FamilyMember_id, string MaritalStatus, 
            string RelationshipToInsured, string EmpNumber, string FirstName, string LastName, 
            DateTime DateOfBirth, string Gender)
        {
            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Where(i => i.RelationshipToInsured == "Spouse")
                .Single();

            sp.RelationshipToInsured = RelationshipToInsured;
            sp.SSN = EmpNumber;
            sp.FirstName = FirstName;
            sp.LastName = LastName;
            sp.DateOfBirth = DateOfBirth;
            sp.Gender = Gender;

            int result = sp.Employee_id;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);

        }

        //SpContact Method
        public ActionResult SpContact()
        {
            return View();
        }

        public JsonResult SpEnrollmentContact(int Employee_id, int FamilyMember_id, string MailingAddress,
            string PObox, string City, string State, string ZipCode, string County, string PhysicalAddress,
            string City2, string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone)
        {
            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            sp.MailingAddress = MailingAddress;
            sp.PObox = PObox;
            sp.City = City;
            sp.State = State;
            sp.ZipCode = ZipCode;
            sp.County = County;
            sp.PhysicalAddress = PhysicalAddress;
            sp.City = City2;
            sp.State = State2;
            sp.ZipCode = ZipCode2;
            sp.EmailAddress = EmailAddress;
            sp.PhoneNumber = PhoneNumber;
            sp.CellPhone = CellPhone;

            int result = sp.Employee_id;    

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //SpEmployment Method
        public ActionResult SpEmployment()
        {
            return View();
        }

        public JsonResult SpEmploymentUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus,
            string Employer, string EmployerAddress, string EmployerPObox, string EmployerCity, 
            string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
        {
            Family_Info sp = db.Family_Info
                .Where(i => i.FamilyMember_id == FamilyMember_id)
                .Single();

            sp.Employer = Employer;
            sp.EmployerMailingAddress = EmployerAddress;
            sp.EmployerPObox = EmployerPObox;
            sp.EmployerCity = EmployerCity;
            sp.EmployerState = EmployerState;
            sp.EmployerZipCode = EmployerZipCode;
            sp.EmployerPhoneNumber = EmployerPhoneNumber;

            if (ModelState.IsValid)
            {
                db.Family_Info.Add(sp);
                db.SaveChanges();
            }

            int result = sp.Employee_id;

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

            ViewBag.Family_Info = f.Employee_id;

            return View(f);
        }

        //SpEditUpdate Method
        public JsonResult SpEditUpdate(int Employee_id, int FamilyMember_id, string MaritalStatus, 
            string RelationshipToInsured, string EmpNumber, string FirstName, string LastName, 
            DateTime DateOfBirth, string Gender, string MailingAddress, string PObox, string City,
            string State, string ZipCode, string County, string PhysicalAddress, string City2, 
            string State2, string ZipCode2, string EmailAddress, string PhoneNumber, string CellPhone,
            string Employer, string EmployerAddress, string EmployerPObox, string EmployerCity,
            string EmployerState, string EmployerZipCode, string EmployerPhoneNumber)
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
            sp.City = City2;
            sp.State = State2;
            sp.ZipCode = ZipCode2;
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

            ViewBag.Employee_id = sp.Employee_id;
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

                RedirectToAction("FamilyOverview", new { sp.Employee_id });
            }

            int result = sp.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //SpDetail Method
        public ActionResult SpDetail(int Employee_id, int? FamilyMember_id)
        {
            //ViewBag.spouseExist = !(MaritalStatus == "Single" || MaritalStatus == "SinglewDep");

            if (FamilyMember_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Family_Info f = db.Family_Info.Find(FamilyMember_id);
            if (family == null)
            {
                return HttpNotFound();
            }

            ViewBag.Family_info = f.Employee_id;

            return View(f);
        }

        //GetSpDetail Method
        public JsonResult GetSpDetail(int FamilyMember_id)
        {
            var sp = db.Family_Info
                 .Where(i => i.FamilyMember_id == FamilyMember_id)
                 .Single();

            int result = sp.Employee_id;

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
            ViewBag.Family_Info = sp.Employee_id;
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

        public ActionResult DepEnrollment()
        {
            return View();
        }

        //DepEnrollmentNew Method
        public JsonResult DepEnrollmentNew(int Employee_id, int FamilyMember_id, string MaritalStatus,
            string RelationshipToInsured, string DepFirstName, string DepLastName, DateTime DateOfBirth,
            string Gender, string EmpNumber)
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

            ViewBag.Family_Info = dep;
            ViewBag.Employee = emp;

            db.Family_Info.Add(dep);
           
            db.SaveChanges();

            int depResult = dep.Employee_id;
            int empResult = emp.Employee_id;
           

            return Json(new { data = depResult, empResult }, JsonRequestBehavior.AllowGet);
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

        //DepEditUpdate Method
        public JsonResult DepEditUpdate(int Employee_id, int FamilyMember_id, int OtherInsurance_id, 
            string MaritalStatus, string RelationshipToInsured, string DepFirstName, string DepLastName,
            DateTime DateOfBirth, string Gender, string EmpNumber)
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

            ViewBag.Family_Info = dep;
            ViewBag.Employee = emp;
          

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

            int result = dep.Employee_id;

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        //DepDetail Method
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
            ViewBag.Family_Info = dep;
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
